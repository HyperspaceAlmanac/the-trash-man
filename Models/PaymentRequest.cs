using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrashCollector.Models
{
    public class PaymentRequest
    {
        [Key]
        public int Id { get; set; }
        public string SessionID { get; set; }
        [ForeignKey("CompletedPickup")]
        public int PickupId { get; set; }
        public CompletedPickup CompletedPickup { get; set; }
    }
}
