using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashCollector.Data;
using TrashCollector.Models;
using TrashCollector.ViewModels;


namespace TrashCollector.Controllers
{
    public class AdministratorController : Controller
    {

        private bool ranOperations;
        private ApplicationDbContext _context;
        public AdministratorController(ApplicationDbContext context)
        {
            _context = context;
            ranOperations = false;
        }
        public ActionResult Index()
        {
            if (!ranOperations)
            {
                // Run the one time methods here
                // AddUnpaidDeliveries();
                ranOperations = true;
            }
            return View();
        }

        private void AddUnpaidDeliveries()
        {
            DateTime twoWeeksAgo = DateTime.Today.AddDays(-14);
            DateTime placeHolder;
            int daysOffset = 0;
            CompletedPickup pickup;
            foreach (Customer c in _context.Customers)
            {
                daysOffset = 3 - c.PickupDay;
                placeHolder = twoWeeksAgo.AddDays(-daysOffset);
                for (int i = 0; i < 8; i++)
                {
                    pickup = new CompletedPickup { CustomerId = c.Id, OneTimePickup = false, Paid = false, Date = placeHolder };
                    _context.CompletedPickups.Add(pickup);
                    placeHolder = placeHolder.AddDays(-7);
                }
                break;
            }
            _context.SaveChanges();
        }

        private void AddPaidDeliveries()
        {
        }
    }
}
