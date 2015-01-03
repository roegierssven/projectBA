using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.tempui.Viewmodel;
using nmct.ba.cashlessproject.tempui.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace nmct.ba.cashlessproject.tempui.Viewmodel
{
    class CustomersVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "CUSTOMERS"; }
        }

         public CustomersVM() 
        {
        
        GetCustomers();

        }

        private async void GetCustomers()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
               client.GetAsync("http://localhost:65079/api/customers");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);
                }
            }
        }
        private ObservableCollection<Customers> _customers;
        public ObservableCollection<Customers> Customers
        {
            get { return _customers; }
            set { _customers = value; OnPropertyChanged("Customers"); }
        }
        private Customers _selectedCustomer;
        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer= value; OnPropertyChanged("SelectedCustomer");
            if (SelectedCustomer != null)
            {
                Id = SelectedCustomer.ID;
                CustomerName = SelectedCustomer.CustomerName;
                Address = SelectedCustomer.Address;
                Balance = SelectedCustomer.Balance.ToString();
            }
            }
        }

        private int _thickness;

        public int Thickness
        {
            get { return _thickness; }
            set { _thickness = value; }
        }

        private Color _brush;

        public Color Brush
        {
            get { return _brush; }
            set { _brush = value; }
        }
        
        

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        

        private string _customerName;

        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value;
            OnPropertyChanged("CustomerName");
            }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value;
            OnPropertyChanged("Address");
            }
        }
        

        private string _balance;

        public string Balance
        {
            get { return _balance; }
            set {
                
                _balance = value;
            OnPropertyChanged("Balance");
            }
        }
        
        

  

public ICommand SaveBalanceCommand
{
 get { return new RelayCommand(SaveBalance); }
}


public async void SaveBalance()
{
    Customers c = new Customers();
    c.ID = Id;
    c.CustomerName = CustomerName;
    c.Address = Address;
    c.Balance = Convert.ToDecimal(Balance);


    if (c.Balance > 100)
    {
        Thickness = 1;
        Brush = Color.FromRgb(200,0,0);
    }
    else
    {
        Thickness = 0;
        Brush = Color.FromRgb(0,200, 0);
        using (HttpClient client = new HttpClient())
        {
            string customer = JsonConvert.SerializeObject(c);
            HttpResponseMessage response = await
           client.PutAsync("http://localhost:65079/api/customers", new StringContent(customer,
           Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                GetCustomers();
            }
        }
    }
    
    
}
    }
}
