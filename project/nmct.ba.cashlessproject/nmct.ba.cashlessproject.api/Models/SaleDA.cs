using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.model;
using System.Data.Common;
using nmct.ba.cashlessproject.api.Helper;
using System.Data;

namespace nmct.ba.cashlessproject.api.Models
{
    public class SaleDA
    { 
        public static List<Sales> GetSales()
        {
            List<Sales> list = new List<Sales>();

            string sql = "SELECT ID, Timestamp, CustomerID, RegisterID, RegisterName, ProductID, ProductName, Amount, Price, TotalPrice FROM Sales";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;
        }

        public static void InsertSale(Sales s)
        {
            string sql = "INSERT INTO Sales VALUES(@Timestamp, @CustomerID, @RegisterID, @RegisterName, @ProductID, @ProductName, @Amount, @Price, @TotalPrice)";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@Timestamp", DateTimeToUnixTimeStamp(s.Timestamp));
            DbParameter par2 = Database.AddParameter("ConnectionString", "@CustomerID", s.CustomersID);
            DbParameter par3 = Database.AddParameter("ConnectionString", "@RegisterID", s.RegisterID);
            DbParameter par4 = Database.AddParameter("ConnectionString", "@RegisterName", s.RegisterName);
            DbParameter par5 = Database.AddParameter("ConnectionString", "@ProductID", s.ProductID);
            DbParameter par6 = Database.AddParameter("ConnectionString", "@ProductName", s.ProductName);
            DbParameter par7 = Database.AddParameter("ConnectionString", "@Amount", s.Amount);
            DbParameter par8 = Database.AddParameter("ConnectionString", "@Price", s.Price);
            DbParameter par9 = Database.AddParameter("ConnectionString", "@TotalPrice", s.TotalPrice);

            Database.InsertData("ConnectionString", sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
        }

        private static Sales Create(IDataRecord record)
        {
            return new Sales()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Timestamp = UnixTimeStampToDateTime(Int32.Parse(record["Timestamp"].ToString())),
                CustomersID = Int32.Parse(record["CustomerID"].ToString()),
                RegisterID = Int32.Parse(record["RegisterID"].ToString()),
                RegisterName = record["RegisterName"].ToString(),
                ProductID = Int32.Parse(record["ProductID"].ToString()),
                ProductName = record["ProductName"].ToString(),
                Amount = Int32.Parse(record["Amount"].ToString()),
                Price = Decimal.Parse(record["Price"].ToString()),
                TotalPrice = Decimal.Parse(record["TotalPrice"].ToString())
            };
        }

        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private static int DateTimeToUnixTimeStamp(DateTime t)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, t.Kind);
            var unixTimestamp = System.Convert.ToInt32((t - date).TotalSeconds);

            return unixTimestamp;
        }
    }
}