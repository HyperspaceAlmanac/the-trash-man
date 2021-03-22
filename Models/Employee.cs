using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashCollector.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string LoginEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ZipCode { get; set; }
        public bool UseSimulatedDay { get; set; }
        public DateTime? SimulatedDay { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [NotMapped]
        public List<Customer> Completed { get; set; }
        [NotMapped]
        public List<Customer> NeedToCollect { get; set; }
        [NotMapped]
        public string WeekDay { get; set; }
        [NotMapped]
        public int SelectedDay { get; set; }
        [NotMapped]
        public string WeekOf { get; set; }
    }
}
