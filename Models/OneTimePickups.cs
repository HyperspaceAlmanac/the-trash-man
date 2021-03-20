using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class OneTimePickups
    {
        [Key, ForeignKey(nameof(Customer)), Column(Order = 1)]
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
        [Key, Column(Order = 2)]
        public DateTime Date;
    }
}
