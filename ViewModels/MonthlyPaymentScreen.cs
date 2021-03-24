using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashCollector.Models;

namespace TrashCollector.ViewModels
{
    public class MonthlyPaymentScreen
    {
        public Customer customer { get; set; }
        public int OffSet { get; set; }
        public string MonthDisplay { get; set; }
        public int TotalCost { get; set; }
        public List<CompletedPickup> PaidPickups { get; set; }
        public List<CompletedPickup> RemainingPickups { get; set; }
    }
}
