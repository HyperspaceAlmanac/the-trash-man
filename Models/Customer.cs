using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string LoginEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DisplayName("Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        [DisplayName("Pickup Day")]
        public int PickupDay { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool AddressSaved { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM dd yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
        public bool CompletedRegistration { get; set; }

        [NotMapped]
        public SelectList DayOptions { get; set; }
        [NotMapped]
        public string DayOfWeek { get; set; }
        [NotMapped]
        public List<OneTimePickup> oneTimePickups {get; set;}
        [NotMapped]
        public List<CompletedPickup> completedPickups { get; set; }
        [NotMapped]
        public string TodayString { get; set; }
        [NotMapped]
        public bool WeeklyPickup { get; set; }
        [NotMapped]
        public int FeesThisMonth { get; set; }
        [NotMapped]
        public int Offset { get; set; }
        [NotMapped]
        public bool PauseError { get; set; }

    }
}
