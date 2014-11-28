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

        private static Customers Create(IDataRecord record)
        {
            return new Customers()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                CustomerName = record["CustomerName"].ToString(),
                Address = record["Address"].ToString(),
                Picture = record["Picture"].ToString(),
                Balance = decimal.Parse(record["Balance"].ToString())
            };
        }

        public static void EditBalance(Customers c, decimal balance)
        {
            try
            {
                string sql = "UPDATE Customers SET Balance=@balance WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@balance", balance);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@id", c.ID);
                Database.ModifyData("ConnectionString", sql, par1, par2);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

            }
        }
    }
}