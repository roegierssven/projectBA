using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nmct.ba.cashlessproject.model
{
   public class Sales
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private DateTime _timestamp;

        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private int _customersID;

        public int CustomersID
        {
            get { return _customersID; }
            set { _customersID = value; }
        }

        private int _registerID;

        public int RegisterID
        {
            get { return _registerID; }
            set { _registerID = value; }
        }

        private int _productID;

        public int ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }

        private int _amount;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        private string _registerName;

        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }
        
        
        
        
        
        
        
        
        
    }
}
