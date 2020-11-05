using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeFullStack;
using System;

namespace PayrollTests
{
    [TestClass]
    public class UnitTest1
    {
        //UC2
        [TestMethod]
        public void TestMethod1()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            employeeRepo.GetAllEmployee();
        }

        [TestMethod]
        public void TestMethod2()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            bool success = employeeRepo.AddNewEmployee("Terisa", 300, System.DateTime.Now, "Med", "1472583690", "Bangalore", 'F');

            Assert.IsTrue(success);
        }


        //UC3
        [TestMethod]
        public void TestMethod3()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            bool success = employeeRepo.UpdateSalary("Terisa",3000000);
            Assert.IsTrue(success);
        }


        //UC4-Refractor
        [TestMethod]
        public void TestMethod4()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            bool success = employeeRepo.GetEmployeeByName("Terisa");
            Assert.IsTrue(success);
        }
        
        //UC5
        [TestMethod]
        public void TestMethod5()
        {
            EmployeeRepo employeeRepo = new EmployeeRepo();
            bool success = employeeRepo.GetEmployeesInRange(DateTime.Parse("1/12/2019"), DateTime.Parse("1/12/2020"));
            Assert.IsTrue(success);
        }
    }
}
