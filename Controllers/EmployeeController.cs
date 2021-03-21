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
            int employeeId = _context.Employees.Where(c => c.IdentityUserId == identifier).Select(c => c.Id).SingleOrDefault();
            return RedirectToAction(nameof(Details), new { EmployeeInfo = employeeId });
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int EmployeeInfo)
        {
            Employee employee = _context.Employees.Where(e => e.Id == EmployeeInfo).SingleOrDefault();
            DateTime today = DateTime.Today;
            if (!employee.UseSimulatedDay)
            {
                employee.WeekDay = DayNumToWord(DateTime.Today.DayOfWeek);
            } else {
                employee.WeekDay = DayNumToWord(employee.SimulatedDay.Value.DayOfWeek);
                today = employee.SimulatedDay.Value;
            }
            // Set of completed Pickups
            HashSet<int> alreadyPickedUp = new HashSet<int> (_context.CompletedPickups.Where(completed => CompareDays(completed.Date, today) == 0).Select(completed => completed.CustomerId));
            employee.Completed = _context.Customers.Where(c => alreadyPickedUp.Contains(c.Id)).ToList();
            HashSet<int> oneTimePickups = new HashSet<int>(_context.OneTimePickups.Where(p => CompareDays(p.Date, today) == 0).Select(p => p.CustomerId));
            // Find all customers in area with trash collection today
            employee.NeedToCollect = _context.Customers.Where(c => c.ZipCode == employee.ZipCode)
                                 .Where(c => DayNumToWord(c.PickupDay) == employee.WeekDay || oneTimePickups.Contains(c.Id))
                                 .Where(c => !alreadyPickedUp.Contains(c.Id))
                                 .Where(c => c.StartDate == null || !( CompareDays(c.StartDate.Value, today) <= 0 && CompareDays(today, c.EndDate.Value) <= 0)).ToList();
            return View(employee);
        }

        // GET: EmployeeController/RegisterAccount
        public ActionResult RegisterAccount(Employee employee)
        {
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
            return View(employee);
        }

        // POST: EmployeeController/RegisterAccount
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FillOutInformation(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
