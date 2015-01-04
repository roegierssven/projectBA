using nmct.ba.cashlessproject.api.Helper;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Models
{
    public class ErrorlogDA
    {
        public static void InsertErrorlog(Errorlog e)
        {
            string sql = "INSERT INTO Errorlog VALUES(@RegisterID, @Timestamp, @Message, @StackTrace)";
            DbParameter par1 = Database.AddParameter("ConnectionString", "@RegisterID", e.RegisterID);
            DbParameter par2 = Database.AddParameter("ConnectionString", "@Timestamp", e.Timestamp);
            DbParameter par3 = Database.AddParameter("ConnectionString", "@Message", e.Message);
            DbParameter par4 = Database.AddParameter("ConnectionString", "@StackTrace", e.StackTrace);

            Database.InsertData("ConnectionString", sql, par1, par2, par3, par4);
        }
    }
}