using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class EmployeeDA
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        private static Employee Create(IDataRecord record)
        {
            return new Employee()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                EmployeeName = record["EmployeeName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = record["Phone"].ToString()
            };
        }

        public static void Remove(Employee e)
        {
            try
            {
                string sql = "DELETE FROM Employee WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@id", e.ID);
                Database.ModifyData("ConnectionString", sql, par1);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void EditEmployee(Employee p, string employeename, string employeeaddress, string employeeemail, string employeephone)
        {
            try
            {
                string sql = "UPDATE Employee SET EmployeeName=@employeename, Address=@employeeaddress, Email=@employeeemail, Phone=@employeephone WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@employeename", employeename);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@employeeaddress", employeeaddress);
                DbParameter par3 = Database.AddParameter("ConnectionString", "@employeeemail", employeeemail);
                DbParameter par4 = Database.AddParameter("ConnectionString", "@employeephone", employeephone);
                DbParameter par5 = Database.AddParameter("ConnectionString", "@id", p.ID);
                Database.ModifyData("ConnectionString", sql, par1, par2, par3, par4, par5);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
        }
    }
}