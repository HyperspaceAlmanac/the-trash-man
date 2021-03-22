using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TrashCollector.Data;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: CustomerController
        public ActionResult Index()
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            if (!customer.CompletedRegistration) {
                return RedirectToAction(nameof(FillOutInformation), new { CustomerInfo = customer.Id });
            } else {
                return RedirectToAction(nameof(Details), new { CustomerInfo = customer.Id });
            }
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int CustomerInfo)
        {
            Customer c = _context.Customers.Where(c => c.Id == CustomerInfo).SingleOrDefault();
            c.DayOfWeek = DayNumToWord(c.PickupDay);
            c.oneTimePickups = _context.OneTimePickups.Where(p => p.CustomerId == CustomerInfo).ToList();

            DateTime today = DateTime.Today.Date;
            c.completedPickups = _context.CompletedPickups.Where(p => p.CustomerId == CustomerInfo && p.Date.Month == today.Month && p.Date.Year == today.Year).ToList();
            int totalCost = 0;
            foreach (CompletedPickup pickup in c.completedPickups)
            {
                if (!pickup.Paid)
                {
                    if (pickup.OneTimePickup)
                    {
                        totalCost += 10;
                    }
                    else
                    {
                        totalCost += 5;
                    }
                }
            }
            c.FeesThisMonth = totalCost;
            return View(c);
        }

        // GET: CustomerController/RegisterAccount
        public ActionResult RegisterAccount(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = _context.Customers.Where(c => c.IdentityUserId == identifier).Select(c => c.Id).SingleOrDefault();
            return RedirectToAction(nameof(FillOutInformation), new { CustomerId = id });
        }

        // POST: CustomerController/RegisterAccount

        public ActionResult FillOutInformation(int CustomerId)
        {
            Customer customer = _context.Customers.Where(c => c.Id == CustomerId).SingleOrDefault();
            customer.DayOptions = GenerateDaysSelectList(customer.PickupDay);
            return View(customer);
        }

        // POST: CustomerController/RegisterAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillOutInformation(Customer customer)
        {
            try
            {
                customer.CompletedRegistration = true;
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CancelPickup(int CustomerId, DateTime PickupDate)
        {
            try
            {
                var pickups = _context.OneTimePickups.Where(p => p.CustomerId == CustomerId && p.Date == PickupDate);
                // For timing issue of Customer on the page to cancel, but does not cancel until after Employee collects trash
                if (pickups.Count() == 1)
                {
                    _context.OneTimePickups.Remove(pickups.SingleOrDefault());
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public ActionResult RegisterOneTimePickup(bool previousError)
        {
            OneTimePickup pickup = new OneTimePickup();
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int customerId = _context.Customers.Where(c => c.IdentityUserId == identifier).Select(c => c.Id).SingleOrDefault();
            pickup.CustomerId = customerId;
            pickup.PreviousError = previousError;
            pickup.TodaysDate = TodaysDateString();
            return View(pickup);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterOneTimePickup(OneTimePickup pickup)
        {
            try
            {
                // Design decision to let customers select a day same as weekly pickup or during suspended period
                // Customer just cannot select day with another one time pickup in same account
                if (_context.OneTimePickups.Where(p => p.CustomerId == pickup.CustomerId && p.Date == pickup.Date).Count() > 0)
                {
                    return RedirectToAction(nameof(RegisterOneTimePickup), new { previousError = true });
                }
                else
                {
                    _context.OneTimePickups.Add(pickup);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PauseService(int CustomerId)
        {
            Customer customer = _context.Customers.Where(c => c.Id == CustomerId).SingleOrDefault();
            customer.DayOptions = GenerateDaysSelectList(customer.PickupDay);
            
            customer.TodayString = TodaysDateString();
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PauseService(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RestoreService(int CustomerId)
        {
            try
            {
                Customer customer = _context.Customers.Where(c => c.Id == CustomerId).SingleOrDefault();
                customer.StartDate = null;
                customer.EndDate = null;
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Details), new { CustomerInfo = CustomerId });
            }
        }
        // left < right => -1, left = right => 0, left > right => 1
        private int CompareDays(DateTime left, DateTime right)
        {
            if (left.Year > right.Year)
            {
                return 1;
            }
            else if (left.Year < right.Year)
            {
                return -1;
            }
            if (left.Month > right.Month)
            {
                return 1;
            }
            else if (left.Month < right.Month)
            {
                return -1;
            }
            if (left.Day > right.Day)
            {
                return 1;
            }
            else if (left.Day < right.Day)
            {
                return -1;
            }
            return 0;
        }
        private string TodaysDateString()
        {
            int year = DateTime.Today.Year;
            string month = (DateTime.Today.Month > 9 ? "" : "0") + DateTime.Today.Month.ToString();
            string day = (DateTime.Today.Day > 9 ? "" : "0") + DateTime.Today.Day.ToString();
            string date = $"{year}-{month}-{day}";
            return date;
        }
        private string DayNumToWord(int day)
        {
            switch (day)
            {
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                case 7:
                    return "Sunday";
                default:
                    return "Monday";
            }
        }
        private int DayEnumToInt(DayOfWeek day) {
            switch (day)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
                default:
                    return 1;
            }
        }
        private SelectList GenerateDaysSelectList(int day)
        {
            if (day == 0)
            {
                day = 1;
            }
            List<SelectListItem> days = new List<SelectListItem>();
            days.Add(new SelectListItem() { Text = "Monday", Value = "1", Selected = false });
            days.Add(new SelectListItem() { Text = "Tuesday", Value = "2", Selected = false });
            days.Add(new SelectListItem() { Text = "Wednesday", Value = "3", Selected = false });
            days.Add(new SelectListItem() { Text = "Thursday", Value = "4", Selected = false });
            days.Add(new SelectListItem() { Text = "Friday", Value = "5", Selected = false });
            days.Add(new SelectListItem() { Text = "Saturday", Value = "6", Selected = false });
            days.Add(new SelectListItem() { Text = "Sunday", Value = "7", Selected = false });
            days[day - 1].Selected = true;
            return new SelectList(days, "Value", "Text");
        }
    }
}
