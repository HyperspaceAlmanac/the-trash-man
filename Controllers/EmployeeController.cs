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
            Employee c = _context.Employees.Where(c => c.Id == EmployeeInfo).SingleOrDefault();
            return View(c);
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
