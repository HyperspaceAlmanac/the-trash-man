using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace TrashCollector.Models
{
    public class PauseService
    {
        public int CustomerId;
        public DateTime StartDate;
        public DateTime EndDate;
    }
}
