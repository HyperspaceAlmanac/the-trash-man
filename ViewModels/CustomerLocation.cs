using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.ViewModels
{
    public class CustomerLocation
    {
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool NeedsPickup { get; set; }
    }
}
