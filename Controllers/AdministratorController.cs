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
                //AddDeliveries();
                ranOperations = true;
            }
            return View();
        }

        private void AddDeliveries()
        {
            DateTime twoWeeksAgo = DateTime.Today.AddDays(-14);
            DateTime placeHolder;
            int daysOffset = 0;
            CompletedPickup pickup;
            foreach (Customer c in _context.Customers)
            {
                daysOffset = 3 - c.PickupDay;
                placeHolder = twoWeeksAgo.AddDays(-daysOffset);
                if (c.Id != 1021)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        pickup = new CompletedPickup { CustomerId = c.Id, OneTimePickup = false, Paid = false, Date = placeHolder };
                        _context.CompletedPickups.Add(pickup);
                        placeHolder = placeHolder.AddDays(-7);
                    }
                }
                placeHolder = twoWeeksAgo.AddDays(-daysOffset + 1);
                for (int i = 0; i < 8; i++)
                {
                    if (i % 2 == 0)
                    {
                        pickup = new CompletedPickup { CustomerId = c.Id, OneTimePickup = true, Paid = false, Date = placeHolder };
                        _context.CompletedPickups.Add(pickup);
                    }
                    placeHolder = placeHolder.AddDays(-7);
                }
            }
            _context.SaveChanges();
        }
    }
}
