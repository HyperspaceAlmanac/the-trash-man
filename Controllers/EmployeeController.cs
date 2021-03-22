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
using TrashCollector.ViewModels;

namespace TrashCollector.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: EmployeeController
        public ActionResult Index()
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Employee employee = _context.Employees.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            if (!employee.CompletedRegistration)
            {
                return RedirectToAction(nameof(FillOutInformation), new { EmployeeInfo = employee.Id });
            }
            else
            {
                return RedirectToAction(nameof(Details), new { EmployeeInfo = employee.Id });
            }
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int EmployeeInfo)
        {
            Employee employee = _context.Employees.Where(e => e.Id == EmployeeInfo).SingleOrDefault();
            DateTime today = DateTime.Today;
            if (employee.UseSimulatedDay) {
                today = employee.SimulatedDay.Value;
            }
            
            string dayOfWeekString;
            dayOfWeekString = DayNumToWord(today.DayOfWeek);
            employee.WeekDay = dayOfWeekString + ", " + MonthString(today.Month) + $" {today.Day}, {today.Year}";
            
            // Set of completed Pickups on this day
            HashSet<int> alreadyPickedUp = new HashSet<int> (_context.CompletedPickups.Where(c => c.Date.Year == today.Year
                && c.Date.Month == today.Month && c.Date.Day == today.Day).Select(c => c.CustomerId));
            employee.Completed = _context.Customers.Where(c => alreadyPickedUp.Contains(c.Id)).ToList();
            HashSet<int> oneTimePickups = new HashSet<int>(_context.OneTimePickups
                .Where(p=> p.Date.Year == today.Year && p.Date.Month == today.Month && p.Date.Day == today.Day).Select(p => p.CustomerId));
            // Find all customers in area with trash collection today
            employee.NeedToCollect = _context.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList()
                                 .Where(c => DayNumToWord(c.PickupDay) == dayOfWeekString || oneTimePickups.Contains(c.Id))
                                 .Where(c => !alreadyPickedUp.Contains(c.Id))
                                 .Where(c => c.StartDate == null ||
                                 !(CompareDays(c.StartDate.Value, today.Date) >= 0)
                                 && (CompareDays(today.Date, c.EndDate.Value) >= 0)).ToList();
            // Separate weekly and one time pickups here
            foreach (var c in employee.NeedToCollect)
            {
                // Weekly Pickup has priority over one time if on same day
                if (DayNumToWord(c.PickupDay) == dayOfWeekString)
                {
                    c.WeeklyPickup = true;
                }
                else
                {
                    c.WeeklyPickup = false;
                }
            }
            employee.SelectedDay = -1;
            // Remap to display string
            return View(employee);
        }

        public ActionResult WeeklyPlanner(int offset)
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Employee employee = _context.Employees.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            DateTime today = DateTime.Today;

            if (employee.UseSimulatedDay)
            {
                today = employee.SimulatedDay.Value;
            }

            DateTime currentDay = today.AddDays(-DayofWeekOffset(today.Date.DayOfWeek));
            if (offset == -1)
            {
                currentDay = today;
                offset = DayofWeekOffset(today.Date.DayOfWeek);
            }
            else
            {
                currentDay = currentDay.AddDays(offset);
            }
            // String WeekDay for Date and Day of week display
            string dayOfWeekString = DayNumToWord(currentDay.DayOfWeek);
            DateTime firstDayOfWeek = currentDay.AddDays(-DayofWeekOffset(currentDay.Date.DayOfWeek));
            string firstDayOfWeekString = DayNumToWord(firstDayOfWeek.DayOfWeek);

            employee.WeekDay = dayOfWeekString + ", " + MonthString(currentDay.Month) + $" {currentDay.Day}, {currentDay.Year}";
            employee.WeekOf = firstDayOfWeekString + ", " + MonthString(firstDayOfWeek.Month) + $" {firstDayOfWeek.Day}, {firstDayOfWeek.Year}";
            employee.Completed = new List<Customer>();
            employee.NeedToCollect = new List<Customer>();
            employee.SelectedDay = offset;

            // Logic for filling out the two tables

            today = currentDay;
            // Set of completed Pickups on this day
            HashSet<int> alreadyPickedUp = new HashSet<int>(_context.CompletedPickups.Where(c => c.Date.Year == today.Year
               && c.Date.Month == today.Month && c.Date.Day == today.Day).Select(c => c.CustomerId));
            employee.Completed = _context.Customers.Where(c => alreadyPickedUp.Contains(c.Id)).ToList();
            HashSet<int> oneTimePickups = new HashSet<int>(_context.OneTimePickups
                .Where(p => p.Date.Year == today.Year && p.Date.Month == today.Month && p.Date.Day == today.Day).Select(p => p.CustomerId));
            // Find all customers in area with trash collection today
            employee.NeedToCollect = _context.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList()
                                 .Where(c => DayNumToWord(c.PickupDay) == dayOfWeekString || oneTimePickups.Contains(c.Id))
                                 .Where(c => !alreadyPickedUp.Contains(c.Id))
                                 .Where(c => c.StartDate == null ||
                                 !(CompareDays(c.StartDate.Value, today.Date) >= 0)
                                 && (CompareDays(today.Date, c.EndDate.Value) >= 0)).ToList();
            // Separate weekly and one time pickups here
            foreach (var c in employee.NeedToCollect)
            {
                // Weekly schedule pickup has priority over one time schedule
                if (DayNumToWord(c.PickupDay) == DayNumToWord(today.DayOfWeek))
                {
                    c.WeeklyPickup = true;
                }
                else
                {
                    c.WeeklyPickup = false;
                }
            }

            return View(employee);

        }
        public ActionResult CompletePickup(int CustomerId, int EmployeeId, bool weeklyPickup)
        {
            Employee employee = _context.Employees.Where(e => e.Id == EmployeeId).FirstOrDefault();
            DateTime today = DateTime.Today;
            if (employee.UseSimulatedDay)
            {
                today = employee.SimulatedDay.Value;
            }
            CompletedPickup pickup = new CompletedPickup();
            pickup.CustomerId = CustomerId;
            pickup.Date = today;
            pickup.OneTimePickup = !weeklyPickup;
            pickup.Paid = false;
            // Check if it is weekly schedule

            var pickupSearch = _context.CompletedPickups.Where(p => p.CustomerId == CustomerId &&
                p.Date.Year == today.Year && p.Date.Month == today.Month && p.Date.Day == today.Day);
            if (pickupSearch.Count() == 0)
            {
                _context.Add(pickup);
                _context.SaveChanges();
            }
            if (!weeklyPickup)
            {
                var oneTimeSearch = _context.OneTimePickups.Where(p => p.CustomerId == CustomerId
                    && p.Date.Year == today.Year && p.Date.Month == today.Month && p.Date.Day == today.Day);
                if (oneTimeSearch.Count() == 1)
                {
                    _context.OneTimePickups.Remove(oneTimeSearch.SingleOrDefault());
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: EmployeeController/RegisterAccount
        public ActionResult RegisterAccount(Employee employee)
        {
            employee.UseSimulatedDay = false;
            _context.Employees.Add(employee);
            _context.SaveChanges();
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = _context.Employees.Where(c => c.IdentityUserId == identifier).Select(c => c.Id).SingleOrDefault();
            return RedirectToAction(nameof(FillOutInformation), new { EmployeeId = id });
        }

        // POST: EmployeeController/RegisterAccount

        public ActionResult FillOutInformation(int EmployeeId)
        {
            Employee employee = _context.Employees.Where(c => c.Id == EmployeeId).SingleOrDefault();
            if (employee == null)
            {
                // For when registration is interrupted
                string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
                employee = _context.Employees.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            }
            return View(employee);
        }

        // POST: EmployeeController/RegisterAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillOutInformation(Employee employee)
        {
            try
            {
                employee.CompletedRegistration = true;
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CustomerProfile(int CustomerId, bool NeedsPickup, int Offset)
        {
            Customer customer = _context.Customers.FirstOrDefault(c => c.Id == CustomerId);
            CustomerLocation profile = new CustomerLocation();
            profile.Name = customer.FirstName + " " + customer.LastName;
            profile.NeedsPickup = NeedsPickup;
            profile.Offset = Offset;
            GetGeoLocation(customer, profile);
            return View(profile);
        }

        private void GetGeoLocation(Customer customer, CustomerLocation profile)
        {
            string originalAddress = $"{customer.StreetAddress}, {customer.City}, {customer.State}";
            profile.FullAddress = originalAddress + " " + customer.ZipCode;
            string formmattedFullURI = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key=" + Secrets.GoogleMapsAPIKey + "&address={0}&sensor=false", Uri.EscapeDataString(originalAddress));
            profile.Longitude = 0;
            profile.Latitude = 0;
        }

        public ActionResult MapsAPI()
        {
            return View();
        }
        private string DayNumToWord(DayOfWeek val)
        {
            switch (val)
            {
                case DayOfWeek.Monday:
                    return "Monday";
                case DayOfWeek.Tuesday:
                    return "Tuesday";
                case DayOfWeek.Wednesday:
                    return "Wednesday";
                case DayOfWeek.Thursday:
                    return "Thursday";
                case DayOfWeek.Friday:
                    return "Friday";
                case DayOfWeek.Saturday:
                    return "Saturday";
                case DayOfWeek.Sunday:
                    return "Sunday";
                default:
                    return "Monday";
            }
        }
        private int DayofWeekOffset(DayOfWeek val)
        {
            switch (val)
            {
                case DayOfWeek.Monday:
                    return 0;
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
                default:
                    return 0;
            }
        }
        private string DayNumToWord(int val)
        {
            switch (val)
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
        private string MonthString(int month)
        {
            switch (month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "January";
            }
        }
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
        private string DateToString(DateTime dateValue)
        {
            int year = dateValue.Date.Year;
            string month = (dateValue.Month > 9 ? "" : "0") + dateValue.Month.ToString();
            string day = (dateValue.Day > 9 ? "" : "0") + dateValue.Day.ToString();
            string date = $"{year}-{month}-{day}";
            return date;
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
