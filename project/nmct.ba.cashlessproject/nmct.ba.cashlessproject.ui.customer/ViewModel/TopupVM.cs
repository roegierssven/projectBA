using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.customer.ViewModel
{
    class TopupVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Topup"; }
        }

        string mname = "TopupVM";
        public TopupVM()
        {
        }

        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set {
                _customerName = value;
                OnPropertyChanged("CustomerName");
            }
        }
        
        private Customers _customer;
        public Customers Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
                NewBalance = Customer.Balance;
            }
        }        

        private ObservableCollection<Customers> _customers;
        public ObservableCollection<Customers> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private decimal _newBalance;
        public decimal NewBalance
        {
            get { return _newBalance; }
            set
            {
                _newBalance = value;
                OnPropertyChanged("NewBalance");
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private Boolean _updated = false;

        public Boolean Updated
        {
            get { return _updated; }
            set
            {
                _updated = value;
                OnPropertyChanged("Updated");
            }
        }

        public ICommand Add5Command
        {
            get { return new RelayCommand(Add5); }
        }

        private void Add5()
        {
            if ((Amount + 5) <= 100)
            {
                Amount += 5;
                NewBalance += 5;
            }               
        }

        public ICommand Add10Command
        {
            get { return new RelayCommand(Add10); }
        }

        private void Add10()
        {
            if ((Amount + 10) <= 100)
            {
                Amount += 10;
                NewBalance += 10;
            }
        }

        public ICommand Add20Command
        {
            get { return new RelayCommand(Add20); }
        }

        private void Add20()
        {
            if ((Amount + 20) <= 100)
            {
                Amount += 20;
                NewBalance += 20;
            }
        }

        public ICommand Add50Command
        {
            get { return new RelayCommand(Add50); }
        }

        private void Add50()
        {
            if ((Amount + 50) <= 100)
            {
                Amount += 50;
                NewBalance += 50;
            }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(Delete); }
        }

        private void Delete()
        {
            Amount = 0;
            NewBalance = Customer.Balance;
        }

        public ICommand UpdateCustomerCommand
        {
            get { return new RelayCommand(UpdateCustomer); }
        }

        public async void UpdateCustomer()
        {
            if (Updated == false)
            {
                Customer.Balance = Convert.ToDecimal(NewBalance);

                using (HttpClient client = new HttpClient())
                {
                    string customer = JsonConvert.SerializeObject(Customer);
                    HttpResponseMessage response = await
                    client.PutAsync("http://localhost:65079/api/Customers", new StringContent(customer,
                    Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        Amount = 0;
                        Updated = true;
                        GetCustomer();
                    }
                    else
                    {
                        MakeErrorLog("There went something wrong with updating the balance", mname, "UpdateCustomer");
                    }
                }
            }
            else
            {
                MakeErrorLog("Customer" + Customer.CustomerName + "Tried for the second time to top up some money.", mname, "UpdateCustomer");
            }          
        }

        private async void GetCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/Customers");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);

                    Customers inDB = null;
                    foreach (Customers c in Customers)
                    {
                        if (c.CustomerName == Customer.CustomerName)
                            inDB = c;
                    }

                    if (inDB != null)
                    {
                        Customer = inDB;
                    }
                    else
                    {
                        MakeErrorLog("Customer needs to registrate", mname, "GetCustomer");
                    }
                }
                else
                {
                    MakeErrorLog("Couldn't get the customer out of the db", mname, "GetCustomer");
                }
            }
        }

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new LoginVM());
        }

        private void MakeErrorLog(string message, string classStackTrace, string methodStackTrace)
        {
            Errorlog errorLog = new Errorlog();
            errorLog.RegisterID = 0;
            errorLog.Timestamp = DateTimeToUnixTimeStamp(DateTime.Now);
            errorLog.Message = message;
            errorLog.StackTrace = "Class: " + classStackTrace + " Method: " + methodStackTrace;

            AddErrorlog(errorLog);
        }

        public async void AddErrorlog(Errorlog e)
        {
            using (HttpClient client = new HttpClient())
            {
                string errorlog = JsonConvert.SerializeObject(e);
                HttpResponseMessage response = await
                    client.PostAsync("http://localhost:65079/api/Errorlog", new StringContent(errorlog,
                        Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Error added.");
                }
            }
        }

        private static int DateTimeToUnixTimeStamp(DateTime t)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, t.Kind);
            var unixTimestamp = System.Convert.ToInt32((t - date).TotalSeconds);

            return unixTimestamp;
        }
    }
}
