using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.model
{
   public class Registers
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _registerName;

        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        private string _device;

        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        //private string _purchaseDate;

        //public string PurchaseDate
        //{
        //    get { return _purchaseDate; }
        //    set { _purchaseDate = value; }
        //}

        //private string _expiresDate;

        //public string ExpiresDate
        //{
        //    get { return _expiresDate; }
        //    set { _expiresDate = value; }
        //}
        
        
        
        
        
    }
}
