using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            dayOfWeekString = Utilities.DayNumToWord(today.DayOfWeek);
            employee.WeekDay = dayOfWeekString + ", " + Utilities.MonthString(today.Month) + $" {today.Day}, {today.Year}";
            
            // Set of completed Pickups on this day
            HashSet<int> alreadyPickedUp = new HashSet<int> (_context.CompletedPickups.Where(c => c.Date.Year == today.Year
                && c.Date.Month == today.Month && c.Date.Day == today.Day).Select(c => c.CustomerId));
            employee.Completed = _context.Customers.Where(c => alreadyPickedUp.Contains(c.Id)).ToList();

            employee.Profiles = new List<CustomerLocation>();
            // Populate map
            CustomerLocation tempProfile;
            foreach (Customer c in employee.Completed)
            {
                tempProfile = new CustomerLocation();
                tempProfile.NeedsPickup = false;
                SetProfile(c, tempProfile);
                employee.Profiles.Add(tempProfile);                
            }

            HashSet<int> oneTimePickups = new HashSet<int>(_context.OneTimePickups
                .Where(p=> p.Date.Year == today.Year && p.Date.Month == today.Month && p.Date.Day == today.Day).Select(p => p.CustomerId));
            // Find all customers in area with trash collection today
            employee.NeedToCollect = _context.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList()
                                 .Where(c => Utilities.DayNumToWord(c.PickupDay) == dayOfWeekString || oneTimePickups.Contains(c.Id))
                                 .Where(c => !alreadyPickedUp.Contains(c.Id))
                                 .Where(c => c.StartDate == null ||
                                 !(Utilities.CompareDays(c.StartDate.Value, today.Date) >= 0)
                                 && (Utilities.CompareDays(today.Date, c.EndDate.Value) >= 0)).ToList();
            DateTime sixDaysAgo = today.AddDays(-6);
            // Multi-part complicated ternary operator to get day comparison to work
            var weeklyPickupLastSixDays = _context.CompletedPickups.Where(p => !p.OneTimePickup &&
                (p.Date.Year < sixDaysAgo.Date.Year ? false
                : (p.Date.Month < sixDaysAgo.Date.Month ? false
                : (p.Date.Day < sixDaysAgo.Date.Day ? false : true)))).Select(p => p.CustomerId);
            // Separate weekly and one time pickups here
            foreach (var c in employee.NeedToCollect)
            {
                c.Offset = 0;
                // Weekly Pickup has priority over one time if on same day
                if (Utilities.DayNumToWord(c.PickupDay) == dayOfWeekString)
                {
                    if (weeklyPickupLastSixDays.Contains(c.Id))
                    {
                        if (oneTimePickups.Contains(c.Id))
                        {
                            c.WeeklyPickup = false;
                        } else {
                            c.WeeklyPickup = true;
                        }
                        c.Offset = -1;
                    }
                    else
                    {
                        c.WeeklyPickup = true;
                    }
                }
                else
                {
                    c.WeeklyPickup = false;
                }
                tempProfile = new CustomerLocation();

                if (c.Offset == -1 && c.WeeklyPickup)
                {
                    tempProfile.NeedsPickup = false;
                }
                else
                {
                    tempProfile.NeedsPickup = true;
                }
                SetProfile(c, tempProfile);
                employee.Profiles.Add(tempProfile);
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

            DateTime currentDay = today.AddDays(-Utilities.DayofWeekOffset(today.Date.DayOfWeek));
            if (offset == -1)
            {
                currentDay = today;
                offset = Utilities.DayofWeekOffset(today.Date.DayOfWeek);
            }
            else
            {
                currentDay = currentDay.AddDays(offset);
            }
            if (Utilities.CompareDays(currentDay, today) < 0)
            {
                employee.PreviousDays = true;
            }
            else
            {
                employee.PreviousDays = false;
            }
            // String WeekDay for Date and Day of week display
            string dayOfWeekString = Utilities.DayNumToWord(currentDay.DayOfWeek);
            DateTime firstDayOfWeek = currentDay.AddDays(-Utilities.DayofWeekOffset(currentDay.Date.DayOfWeek));
            string firstDayOfWeekString = Utilities.DayNumToWord(firstDayOfWeek.DayOfWeek);

            employee.WeekDay = dayOfWeekString + ", " + Utilities.MonthString(currentDay.Month) + $" {currentDay.Day}, {currentDay.Year}";
            employee.WeekOf = firstDayOfWeekString + ", " + Utilities.MonthString(firstDayOfWeek.Month) + $" {firstDayOfWeek.Day}, {firstDayOfWeek.Year}";
            employee.Completed = new List<Customer>();
            employee.NeedToCollect = new List<Customer>();
            employee.SelectedDay = offset;

            today = currentDay;
            // Set of completed Pickups on this day
            HashSet<int> alreadyPickedUp = new HashSet<int>(_context.CompletedPickups.Where(c => c.Date.Year == today.Year
               && c.Date.Month == today.Month && c.Date.Day == today.Day).Select(c => c.CustomerId));
            employee.Completed = _context.Customers.Where(c => alreadyPickedUp.Contains(c.Id)).ToList();

            DateTime sixDaysAgo = today.AddDays(-6);
            // Multi-part complicated ternary operator to get day comparison to work
            var weeklyPickupLastSixDays = _context.CompletedPickups.Where(p => !p.OneTimePickup &&
                (p.Date.Year < sixDaysAgo.Date.Year ? false
                : (p.Date.Month < sixDaysAgo.Date.Month ? false
                : (p.Date.Day < sixDaysAgo.Date.Day ? false : true)))).Select(p => p.CustomerId);

            employee.Profiles = new List<CustomerLocation>();
            // Populate map
            CustomerLocation tempProfile;
            foreach (Customer c in employee.Completed)
            {
                tempProfile = new CustomerLocation();
                tempProfile.NeedsPickup = false;
                SetProfile(c, tempProfile);
                employee.Profiles.Add(tempProfile);
            }

            HashSet<int> oneTimePickups = new HashSet<int>(_context.OneTimePickups
                .Where(p => p.Date.Year == today.Year && p.Date.Month == today.Month && p.Date.Day == today.Day).Select(p => p.CustomerId));
            // Find all customers in area with trash collection today
            employee.NeedToCollect = _context.Customers.Where(c => c.ZipCode == employee.ZipCode).ToList()
                                 .Where(c => Utilities.DayNumToWord(c.PickupDay) == dayOfWeekString || oneTimePickups.Contains(c.Id))
                                 .Where(c => !alreadyPickedUp.Contains(c.Id))
                                 .Where(c => c.StartDate == null ||
                                 !(Utilities.CompareDays(c.StartDate.Value, today.Date) >= 0)
                                 && (Utilities.CompareDays(today.Date, c.EndDate.Value) >= 0)).ToList();
            // Separate weekly and one time pickups here
            foreach (var c in employee.NeedToCollect)
            {
                c.Offset = 0;
                // Weekly schedule pickup has priority over one time schedule
                if (Utilities.DayNumToWord(c.PickupDay) == Utilities.DayNumToWord(today.DayOfWeek))
                {
                    if (weeklyPickupLastSixDays.Contains(c.Id))
                    {
                        if (oneTimePickups.Contains(c.Id))
                        {
                            c.WeeklyPickup = false;
                        }
                        else
                        {
                            c.WeeklyPickup = true;
                        }
                        c.Offset = -1;
                    }
                    else
                    {
                        c.WeeklyPickup = true;
                    }
                }
                else
                {
                    c.WeeklyPickup = false;
                }
                tempProfile = new CustomerLocation();
                if (c.Offset == -1 && c.WeeklyPickup)
                {
                    tempProfile.NeedsPickup = false;
                }
                else
                {
                    tempProfile.NeedsPickup = true;
                }
                SetProfile(c, tempProfile);
                employee.Profiles.Add(tempProfile);
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
            profile.Offset = Offset;
            profile.NeedsPickup = NeedsPickup;
            SetProfile(customer, profile);

            return View(profile);
        }
        private void SetProfile(Customer customer, CustomerLocation profile)
        {
            profile.Name = customer.FirstName + " " + customer.LastName;

            profile.FullAddress = $"{customer.StreetAddress}, {customer.City}, {customer.State} {customer.ZipCode}";
            if (customer.AddressSaved)
            {
                profile.GeoLocationSuccess = true;
                profile.Longitude = customer.Longitude;
                profile.Latitude = customer.Latitude;
            }
            else
            {
                profile.GeoLocationSuccess = GetGeoLocation(customer, profile);
                if (profile.GeoLocationSuccess)
                {
                    // Only using and saving geoLocation results for use with Google Maps API
                    customer.AddressSaved = true;
                    customer.Longitude = profile.Longitude;
                    customer.Latitude = profile.Latitude;
                    _context.Update(customer);
                    _context.SaveChanges();
                }
            }
        }

        private bool GetGeoLocation(Customer customer, CustomerLocation profile)
        {
            // Using Code from https://stackoverflow.com/questions/16274508/how-to-call-google-geocoding-service-from-c-sharp-code
            // Using the method in the answer for sending GeoLocation request, and then converting response into XML and reading it
            string originalAddress = $"{customer.StreetAddress}, {customer.City}, {customer.State}";
            string fullURI = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key=" + Secrets.GeoLocationKey + "&address={0}&sensor=false", Uri.EscapeDataString(originalAddress));
            profile.Longitude = 0;
            profile.Latitude = 0;
            WebRequest request;
            WebResponse response;
            try
            {
                request = WebRequest.Create(fullURI);
                response = request.GetResponse();
            }
            catch (WebException)
            {
                return false;
            }
            XDocument xdoc = XDocument.Load(response.GetResponseStream());
            XElement status = xdoc.Element("GeocodeResponse").Element("status");
            if (status.Value != "OK")
            {
                return false;
            }
            XElement location = xdoc.Element("GeocodeResponse").Element("result").Element("geometry").Element("location");
            try
            {
                profile.Longitude = Convert.ToDouble(location.Element("lng").Value);
                profile.Latitude = Convert.ToDouble(location.Element("lat").Value);
            } catch (FormatException) {
                return false;
            }
            return true;

        }
    }
}
