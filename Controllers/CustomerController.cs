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
            int customerId = _context.Customers.Where(c => c.IdentityUserId == identifier).Select(c => c.Id).SingleOrDefault();
            return RedirectToAction(nameof(Details), new { CustomerInfo = customerId });
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int CustomerInfo)
        {
            Customer c = _context.Customers.Where(c => c.Id == CustomerInfo).SingleOrDefault();
            c.DayOfWeek = DayNumToWord(c.PickupDay);
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
                _context.Customers.Update(customer);
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

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
