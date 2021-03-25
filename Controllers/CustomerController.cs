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
                return RedirectToAction(nameof(FillOutInformation));
            } else {
                return RedirectToAction(nameof(Details));
            }
        }
        public ActionResult MonthlyBill(int monthOffSet)
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            MonthlyPaymentScreen monthly = new MonthlyPaymentScreen();
            DateTime targetMonth = FindPrevMonth(monthOffSet);
            monthly.AllPickups = _context.CompletedPickups.Where(p => p.Date.Month == targetMonth.Month && p.Date.Year == targetMonth.Year
                && p.CustomerId == customer.Id).ToList();
            monthly.MonthDisplay = Utilities.MonthString(targetMonth.Month) + ", " + targetMonth.Year;
            monthly.TotalCost = 0;
            monthly.OffSet = monthOffSet;
            foreach (CompletedPickup pickup in monthly.AllPickups)
            {
                if (!pickup.Paid)
                {
                    if (pickup.OneTimePickup)
                    {
                        monthly.TotalCost += 10;
                    }
                    else
                    {
                        monthly.TotalCost += 5;
                    }
                }
            }
            return View(monthly);
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession(int monthOffSet)
        {
            DateTime targetMonth = FindPrevMonth(monthOffSet);
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            
            var pickupsThisMonth = _context.CompletedPickups.Where(p => p.CustomerId == customer.Id && p.Date.Month == targetMonth.Month && p.Date.Year == targetMonth.Year);
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
        private DateTime FindPrevMonth(int offset)
        {
            DateTime today = DateTime.Today;
            int dayOffset = today.Day;
            today = today.AddDays(-dayOffset + 1); // Normalize to first day of month
            if (offset == 0)
            {
                return today;
            }
            today = today.AddMonths(-offset);
            return today;
        }
        public ActionResult Success(string session_id)
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            // Unpaid but Completed Pickups this Month
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            List<CompletedPickup> customerHistory = _context.CompletedPickups.Where(p => p.CustomerId == customer.Id).ToList();
            HashSet<int> thisPayment = new HashSet<int>(_context.PaymentRequests.Where(pr => pr.SessionID == session_id).Select(pr => pr.PickupId));
            foreach (CompletedPickup pickup in customerHistory)
            {
                if (thisPayment.Contains(pickup.Id) && !pickup.Paid)
                {
                    pickup.Paid = true;
                    _context.Update(pickup);
                    _context.SaveChanges();
                }
            }
            return View();
        }

        // GET: CustomerController/Details/5
        public ActionResult Details()
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer c = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            if (c == null)
            {
                return RedirectToAction(nameof(Warning));
            }
            c.DayOfWeek = Utilities.DayNumToWord(c.PickupDay);
            c.oneTimePickups = _context.OneTimePickups.Where(p => p.CustomerId == c.Id).ToList();

            DateTime today = DateTime.Today.Date;
            c.completedPickups = _context.CompletedPickups.Where(p => p.CustomerId == c.Id && p.Date.Month == today.Month && p.Date.Year == today.Year).ToList();
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
            return RedirectToAction(nameof(FillOutInformation));
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
            customer.DayOptions = Utilities.GenerateDaysSelectList(customer.PickupDay);
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
                customer.AddressSaved = false;
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult CancelPickup(DateTime PickupDate)
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            try
            {
                var pickups = _context.OneTimePickups.Where(p => p.CustomerId == customer.Id && p.Date == PickupDate);
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
        public ActionResult PauseService(bool error)
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            if (customer == null)
            {
                return RedirectToAction(nameof(Warning));
            }
            customer.DayOptions = Utilities.GenerateDaysSelectList(customer.PickupDay);
            customer.PauseError = error;
            
            customer.TodayString = TodaysDateString();
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PauseService(Models.Customer customer)
        {
            try
            {
                if (Utilities.CompareDays(customer.StartDate.Value, customer.EndDate.Value) == 1)
                {
                    return RedirectToAction(nameof(PauseService), new { error = true });
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

        public ActionResult RestoreService()
        {
            string identifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Models.Customer customer = _context.Customers.Where(c => c.IdentityUserId == identifier).SingleOrDefault();
            if (customer == null)
            {
                return RedirectToAction(nameof(Warning));
            }
            try
            {
                customer.StartDate = null;
                customer.EndDate = null;
                _context.Customers.Update(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Details));
            }
        }
        public ActionResult Warning()
        {
            return View();
        }

        private string TodaysDateString()
        {
            int year = DateTime.Today.Year;
            string month = (DateTime.Today.Month > 9 ? "" : "0") + DateTime.Today.Month.ToString();
            string day = (DateTime.Today.Day > 9 ? "" : "0") + DateTime.Today.Day.ToString();
            string date = $"{year}-{month}-{day}";
            return date;
        }
    }
}
