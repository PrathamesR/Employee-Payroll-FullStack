using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeFullStack
{
    public class EmployeeRepo
    {
        //static string conn = @"Data Source='(LocalDB)\MSSQL Server';Initial Catalog = Payroll; Integrated Security = True";

        public static SqlConnection getConnect()
        {
            SqlConnection connection = new SqlConnection(@"Data Source='(LocalDB)\MSSQL Server';Initial Catalog = Payroll; Integrated Security = True");
            return connection;
        }

        public bool GetAllEmployee()
        {
            SqlConnection connection = getConnect();
            try
            {

                Employee employee = new Employee();
                using (connection)
                {
                    string query = @"SELECT * FROM employee_payroll";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            employee.EmployeeID = dataReader.GetInt32(0);
                            employee.EmployeeName = dataReader.GetString(1);
                            if(dataReader["phoneNo"]!=DBNull.Value)
                                employee.PhoneNumber = dataReader.GetDecimal(4).ToString();
                            employee.Department = dataReader.GetString(5);
                            employee.Address = dataReader.GetString(6);
                            employee.StartDate = dataReader.GetDateTime(3);
                            employee.Gender = dataReader.GetString(7)[0];
                            employee.BasicPay = Convert.ToInt32(dataReader.GetDecimal(2));

                            Console.WriteLine(employee.ToString());
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+e.StackTrace);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool AddNewEmployee(string name, int basicPay, DateTime date, string department, string phoneNo, string address, char gender)
        {
            SqlConnection connection = null;
            try
            {
                connection = getConnect();
                using (connection)
                {
                    connection.Open();
                    Employee employee = new Employee();
                    SqlCommand command = new SqlCommand("AddNewEmployee", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@basicPay", basicPay);
                    command.Parameters.AddWithValue("@startDate", date);
                    command.Parameters.AddWithValue("@phoneNo", phoneNo);
                    command.Parameters.AddWithValue("@department", department);
                    command.Parameters.AddWithValue("@address", address);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@deductions", basicPay / 20);
                    command.Parameters.AddWithValue("@taxable_pay", basicPay - basicPay / 20);
                    command.Parameters.AddWithValue("@income_Tax", (basicPay - basicPay / 20) / 10);
                    command.Parameters.AddWithValue("@net_pay", basicPay - ((basicPay - basicPay / 20) / 10));

                    SqlDataReader dr = command.ExecuteReader();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool GetEmployeeByName(string name)
        {

            SqlConnection connection = getConnect();
            try
            {
                Employee employee = new Employee();
                using (connection)
                {
                    string query = @"SELECT * FROM employee_payroll where name='"+name+"'";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            employee.EmployeeID = dataReader.GetInt32(0);
                            employee.EmployeeName = dataReader.GetString(1);
                            if (dataReader["phoneNo"] != DBNull.Value)
                                employee.PhoneNumber = dataReader.GetDecimal(4).ToString();
                            employee.Department = dataReader.GetString(5);
                            employee.Address = dataReader.GetString(6);
                            employee.StartDate = dataReader.GetDateTime(3);
                            employee.Gender = dataReader.GetString(7)[0];
                            employee.BasicPay = Convert.ToInt32(dataReader.GetDecimal(2));

                            Console.WriteLine(employee.ToString());
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateSalary(string name, int basicPay)
        {
            SqlConnection connection = null;
            try
            {
                connection = getConnect();
                using (connection)
                {
                    connection.Open();
                    Employee employee = new Employee();
                    SqlCommand command = new SqlCommand("UpdateSalary", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@basicPay", basicPay);

                    SqlDataReader dr = command.ExecuteReader();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool GetEmployeesInRange(DateTime startDate, DateTime endDate)
        {
            SqlConnection connection = null;
            try
            {
                connection = getConnect();
                using (connection)
                {
                    connection.Open();
                    Employee employee = new Employee();
                    SqlCommand command = new SqlCommand("GetEmployeesJoinedInRange", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@startDate", startDate.Date.ToShortDateString()); ;
                    command.Parameters.AddWithValue("@endDate", startDate.Date.ToShortDateString());

                    SqlDataReader dr = command.ExecuteReader();

                    if(dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            employee.EmployeeID = dr.GetInt32(0);
                            employee.EmployeeName = dr.GetString(1);
                            if (dr["phoneNo"] != DBNull.Value)
                                employee.PhoneNumber = dr.GetDecimal(4).ToString();
                            employee.Department = dr.GetString(5);
                            employee.Address = dr.GetString(6);
                            employee.StartDate = dr.GetDateTime(3);
                            employee.Gender = dr.GetString(7)[0];
                            employee.BasicPay = Convert.ToInt32(dr.GetDecimal(2));

                            Console.WriteLine(employee.ToString());
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool GetStatsByGender()
        {
            SqlConnection connection = getConnect();
            try
            {

                Employee employee = new Employee();
                using (connection)
                {
                    string query = @"select gender,SUM(basic_Pay),AVG(basic_Pay),MIN(basic_Pay),MAX(basic_Pay),COUNT(basic_Pay) from employee_payroll group by gender";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader dataReader = cmd.ExecuteReader();

                    //Check if there are records
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            Console.WriteLine(dataReader.GetString(0) + ":- Sum=" + dataReader.GetDecimal(1) + " Average=" + dataReader.GetDecimal(2) + " Minimum=" + dataReader.GetDecimal(3) + " Count=" + dataReader.GetDecimal(4));
                            Console.WriteLine(employee.ToString());
                        }
                    }
                    else
                        Console.WriteLine("Has no data");
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
