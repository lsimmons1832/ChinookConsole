using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ChinookConsoleApp
{
    class UpdateEmployee
    {
        public void Update()
        {
            var employeeList = new ListEmployees();
            var ChangeEmployee = employeeList.List("Pick an employee to update:");
            Console.WriteLine("Enter the new Last name");
            var newName = Console.ReadLine();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "Update Employee set LastName = @newName where EmployeeId = @EmployeeId";

                var employeeIdParameter = cmd.Parameters.Add("@EmployeeId", SqlDbType.Int);
                employeeIdParameter.Value = ChangeEmployee;

                var employeeNameParameter = cmd.Parameters.Add("@newName", SqlDbType.VarChar);
                employeeNameParameter.Value = newName;

                try
                {
                    var affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows == 1)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Failed to find a matching Id");
                    }

                    Console.WriteLine("Press enter to return to the menu");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
