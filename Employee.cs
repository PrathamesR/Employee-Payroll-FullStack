using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeFullStack
{
    class Employee
    {
        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string PhoneNumber {get;set;}
        public string Department { get; set; }
        public DateTime? StartDate { get; set; }
        public string Address { get; set; }
        public char Gender { get; set; }
        public int BasicPay { get; set; }

        public Employee()
        {
        }

        public override string ToString()
        {
            return EmployeeID + " " + this.EmployeeName + " " + this.Address + " " + this.PhoneNumber + " " + this.StartDate + " " + this.Gender + " " + this.BasicPay;
        }
    }    
}
