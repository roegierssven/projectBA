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
    public class Register_EmployeeDA
    {
        
        public static List<Register_Employee> GetRegister_Employees()
        {
            List<Register_Employee> list = new List<Register_Employee>();

            string sql = "SELECT RegisterID, EmployeeID, Vanaf, Until FROM Register_Employee";
            DbDataReader reader = Database.GetData("ConnectionString", sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }

            reader.Close();
            return list;
        }

       
        public static List<Register_Employee> GetRegister_Employee(int id)
        {
            List<Register_Employee> list = new List<Register_Employee>();

            string sql = "SELECT RegisterID, EmployeeID, Vanaf, Until FROM Register_Employee WHERE RegisterID=@ID";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@ID", id);
            DbDataReader reader = Database.GetData("ConnectionString", sql, par1);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        
        public static void AddRegisterEmployee(Register_Employee r)
        {
            string sql = "INSERT INTO Register_Employee VALUES(@RegisterID, @EmployeeID, @Vanaf, @Until)";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@RegisterID", r.RegisterID);
            DbParameter par2 = Database.AddParameter("ConnectionString", "@EmployeeID", r.EmployeeID);
            DbParameter par3 = Database.AddParameter("ConnectionString", "@Vanaf", DateTimeToUnixTimeStamp(r.Vanaf));
            DbParameter par4 = Database.AddParameter("ConnectionString", "@Until", DateTimeToUnixTimeStamp(r.Until));

            Database.InsertData("ConnectionString", sql, par1, par2, par3, par4);
        }

        
        private static Register_Employee Create(IDataRecord record)
        {
            return new Register_Employee()
            {
                RegisterID = Int32.Parse(record["RegisterID"].ToString()),
                RegisterName = RegisterDA.GetRegister(Int32.Parse(record["RegisterID"].ToString())).RegisterName,
                EmployeeID = Int32.Parse(record["EmployeeID"].ToString()),
                EmployeeName = EmployeeDA.GetEmployee(Int32.Parse(record["EmployeeID"].ToString())).EmployeeName,
                Vanaf = UnixTimeStampToDateTime(Int32.Parse(record["Vanaf"].ToString())),
                Until = UnixTimeStampToDateTime(Int32.Parse(record["Until"].ToString()))
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