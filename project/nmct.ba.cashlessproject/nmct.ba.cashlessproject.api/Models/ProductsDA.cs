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
    public class ProductsDA
    {

        public static List<Products> GetProducts()
        {
            List<Products> list = new List<Products>();

            string sql = "SELECT ID, ProductName, Price FROM Products";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        private static Products Create(IDataRecord record)
        {
            return new Products()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                ProductName = record["ProductName"].ToString(),
                Price = Decimal.Parse(record["Price"].ToString())
            };
        }

        public static void AddProduct(Products p)
        {
            try
            {
                string sql = "INSERT INTO Products (ProductName, Price) VALUES (@name, @price);";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@name", p.ProductName);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@price", p.Price);
                Database.ModifyData("ConnectionString", sql, par1, par2);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
        public static void Remove(Products p)
        {
            try
            {
                string sql = "DELETE FROM Products WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@id", p.ID);
                Database.ModifyData("ConnectionString", sql, par1);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static void EditProduct(Products p, string productname, decimal productprice)
        {
            try
            {
                string sql = "UPDATE Products SET ProductName=@productname, Price=@productprice WHERE ID=@id";
                DbParameter par1 = Database.AddParameter("ConnectionString", "@productname", productname);
                DbParameter par2 = Database.AddParameter("ConnectionString", "@productprice", productprice);
                DbParameter par3 = Database.AddParameter("ConnectionString", "@id", p.ID);
                Database.ModifyData("ConnectionString", sql, par1, par2, par3);

            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                
            }
        }
    }
}