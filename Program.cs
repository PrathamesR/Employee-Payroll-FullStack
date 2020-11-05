using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeFullStack
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepo repo = new EmployeeRepo();
            /*
           // repo.AddNewEmployee("abc", 30000, DateTime.Now, "Mech", "4781592360", "Thane", 'M');
            Console.WriteLine("sfadf");
            repo.GetAllEmployee();
            Console.Read();
            */
            Console.WriteLine(DateTime.Now.Date.ToShortDateString());
            Console.Read();
        }
    }
}
