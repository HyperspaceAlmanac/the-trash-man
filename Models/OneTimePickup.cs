using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class OneTimePickup
    {
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [NotMapped]
        public bool PreviousError { get; set; }
        [NotMapped]
        public string TodaysDate { get; set; }

    }
}