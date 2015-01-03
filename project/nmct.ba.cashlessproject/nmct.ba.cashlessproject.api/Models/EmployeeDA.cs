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

        public static Employee GetEmployee(int id)
        {
            string sql = "SELECT ID, EmployeeName, Address, Email, Phone FROM Employee WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@ID", id);
            DbDataReader reader = Database.GetData("ConnectionString", sql, par1);
            Employee result = null;

            while (reader.Read())
            {
                result = Create(reader);
            }
            reader.Close();
            return result;
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

        public static void AddEmployee(Employee e)
        {
            try
            {
                string sql = "INSERT INTO Employee (EmployeeName, Address, Email, Phone) VALUES (@name, @address, @email, @phone);";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@name", e.EmployeeName);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@address", e.Address);
                DbParameter par3 = Database.AddParameter("ConnectionString", "@email", e.Email);
                DbParameter par4 = Database.AddParameter("ConnectionString", "@phone", e.Phone);
                Database.InsertData("ConnectionString", sql, par1, par2, par3, par4);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void Remove(int id)
        {
            try
            {
                string sql = "DELETE FROM Employee WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@id", id);
                Database.ModifyData("ConnectionString", sql, par1);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void EditEmployee(Employee e)
        {
            try
            {
                string sql = "UPDATE Employee SET EmployeeName=@employeename, Address=@employeeaddress, Email=@employeeemail, Phone=@employeephone WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@employeename", e.EmployeeName);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@employeeaddress", e.Address);
                DbParameter par3 = Database.AddParameter("ConnectionString", "@employeeemail", e.Email);
                DbParameter par4 = Database.AddParameter("ConnectionString", "@employeephone", e.Phone);
                DbParameter par5 = Database.AddParameter("ConnectionString", "@id", e.ID);
                Database.ModifyData("ConnectionString", sql, par1, par2, par3, par4, par5);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
        }
    }
}