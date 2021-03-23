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
using Stripe;
using Stripe.Checkout;
using System.Web;

namespace TrashCollector.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
            StripeConfiguration.ApiKey = Secrets.StripeSecretKey;

        }
        // GET: CustomerController
        public ActionResult Index()
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            if (!customer.CompletedRegistration) {
                return RedirectToAction(nameof(FillOutInformation), new { CustomerInfo = customer.Id });
            } else {
                return RedirectToAction(nameof(Details), new { CustomerInfo = customer.Id });
            }
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            DateTime today = DateTime.Today.Date;

            var pickupsThisMonth = _context.CompletedPickups.Where(p => p.CustomerId == customer.Id && p.Date.Month == today.Month && p.Date.Year == today.Year);
            int totalCost = 0;
            int port = this.HttpContext.Connection.LocalPort;

            // Get port
            List<int> chargedPickups = new List<int>();
            foreach (CompletedPickup pickup in pickupsThisMonth)
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
                    chargedPickups.Add(pickup.Id);
                }
            }

            var options = new SessionCreateOptions
            {
                CustomerEmail = customer.LoginEmail,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = totalCost * 100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Trash Collection Service Payment",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:" + port + "/Customer/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:" + port + "/Customer/Index"

            };

            var service = new SessionService();
            Session session = service.Create(options);

            // Now update the database
            PaymentRequest request;
            foreach (int id in chargedPickups)
            {
                request = new PaymentRequest();
                request.SessionID = session.Id;
                request.PickupId = id;
                _context.Add(request);
                _context.SaveChanges();
            }

            return Json(new { id = session.Id });
        }
        public ActionResult Success(string session_id)
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            // Unpaid but Completed Pickups this Month
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            List<CompletedPickup> unpaidThisMonth = _context.CompletedPickups.Where(p => p.CustomerId == customer.Id
                && p.Date.Year == year && p.Date.Month == month && p.Paid == false).ToList();
            HashSet<int> thisPayment = new HashSet<int>(_context.PaymentRequests.Where(pr => pr.SessionID == session_id).Select(pr => pr.PickupId));
            foreach (CompletedPickup pickup in unpaidThisMonth)
            {
                if (thisPayment.Contains(pickup.Id))
                {
                    pickup.Paid = true;
                    _context.Update(pickup);
                    _context.SaveChanges();
                }
            }
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int CustomerInfo)
        {
            Models.Customer c = _context.Customers.Where(c => c.Id == CustomerInfo).SingleOrDefault();
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
        public ActionResult RegisterAccount(Models.Customer customer)
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
            Models.Customer customer = _context.Customers.Where(c => c.Id == CustomerId).SingleOrDefault();
            if (customer == null)
            {
                // For when registration process is interrupted
                string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            } 
            customer.DayOptions = GenerateDaysSelectList(customer.PickupDay);
            return View(customer);
        }

        // POST: CustomerController/RegisterAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillOutInformation(Models.Customer customer)
        {
            try
            {
                customer.CompletedRegistration = true;
                Models.Customer originalRow = _context.Customers.Where(c => c.Id == customer.Id).FirstOrDefault();
                if (originalRow.StreetAddress != customer.StreetAddress
                    || originalRow.ZipCode != customer.ZipCode
                    || originalRow.State != customer.State
                    || originalRow.City != customer.City)
                {
                    customer.AddressSaved = false; // Invalidate saved Longitude and Latitude if information changed
                }
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
            Models.Customer customer = _context.Customers.Where(c => c.Id == CustomerId).SingleOrDefault();
            customer.DayOptions = GenerateDaysSelectList(customer.PickupDay);
            
            customer.TodayString = TodaysDateString();
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PauseService(Models.Customer customer)
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
                Models.Customer customer = _context.Customers.Where(c => c.Id == CustomerId).SingleOrDefault();
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
