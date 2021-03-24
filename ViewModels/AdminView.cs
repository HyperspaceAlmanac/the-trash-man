using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrashCollector.Models;

namespace TrashCollector.ViewModels
{
    public class AdminView
    {
        public string Name { get; set; }
        public List<Customer> CustomerAccounts { get; set; }
        public List<Employee> EmployeeAccounts { get; set; }
        public string StatusMessage { get; set; }
    }
}
