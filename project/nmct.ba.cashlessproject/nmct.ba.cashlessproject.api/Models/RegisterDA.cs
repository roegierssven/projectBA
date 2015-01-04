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
    public class RegisterDA
    {
        public static List<Registers> GetRegisters()
        {
            List<Registers> list = new List<Registers>();

            string sql = "SELECT ID, RegisterName, Device FROM Registers";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        public static Registers GetRegister(int id)
        {
            string sql = "SELECT ID, RegisterName, Device FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@ID", id);
            DbDataReader reader = Database.GetData("ConnectionString", sql, par1);
            Registers result = null;

            while (reader.Read())
            {
                result = Create(reader);
            }
            reader.Close();
            return result;
        }

        private static Registers Create(IDataRecord record)
        {
            return new Registers()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                RegisterName = record["RegisterName"].ToString(),
                Device = record["Device"].ToString(),
                
            };
        }
    }
}