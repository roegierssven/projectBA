using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.model
{
   public class Register_Employee
    {

        private int _registerID;
        public int RegisterID
        {
            get { return _registerID; }
            set { _registerID = value; }
        }

        private string _registerName;
        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }


        private int _employeeID;
        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value; }
        }


        private DateTime _vanaf;
        public DateTime Vanaf
        {
            get { return _vanaf; }
            set { _vanaf = value; }
        }

        private DateTime _until;
        public DateTime Until
        {
            get { return _until; }
            set { _until = value; }
        }
    }
}
