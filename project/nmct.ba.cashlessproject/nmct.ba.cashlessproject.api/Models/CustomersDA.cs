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
    public class CustomersDA
    {
        
        public static List<Customers> GetCustomers()
        {
            List<Customers> list = new List<Customers>();

            string sql = "SELECT ID, CustomerName, Address, Picture, Balance FROM Customers";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;
        }

       
        public static Customers GetCustomer(int id)
        {
            string sql = "SELECT ID, CustomerName, Address, Picture, Balance FROM Customers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@ID", id);
            DbDataReader reader = Database.GetData("ConnectionString", sql, par1);
            Customers result = null;

            while (reader.Read())
            {
                result = Create(reader);
            }
            reader.Close();
            return result;
        }

        
        public static void InsertCustomer(Customers c)
        {
            string sql = "INSERT INTO Customers VALUES(@CustomerName, @Address, @Picture, @Balance)";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.AddParameter("ConnectionString", "@Address", c.Address);
            DbParameter par3 = Database.AddParameter("ConnectionString", "@Picture", c.Picture);
            DbParameter par4 = Database.AddParameter("ConnectionString", "@Balance", c.Balance);

            Database.InsertData("ConnectionString", sql, par1, par2, par3, par4);
        }

        
        public static void EditCustomer(Customers c)
        {
            try
            {
                string sql = "UPDATE Customers SET Balance=@Balance WHERE ID=@ID";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@Balance", c.Balance);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@ID", c.ID);
                Database.ModifyData("ConnectionString", sql, par1, par2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //create customer
        private static Customers Create(IDataRecord record)
        {
            byte[] bytes = null;
            if (record["Picture"] != null && record["Picture"] != DBNull.Value && record["Picture"] is byte[])
            {
                bytes = (byte[])record["Picture"];
            }

            return new Customers()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                CustomerName = record["CustomerName"].ToString(),
                Address = record["Address"].ToString(),
                Picture = bytes,
                Balance = Decimal.Parse(record["Balance"].ToString())
            };
        }
    }
}