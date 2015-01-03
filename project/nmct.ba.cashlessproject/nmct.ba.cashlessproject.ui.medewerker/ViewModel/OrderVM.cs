
using be.belgium.eid;
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
using System.Windows;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.medewerker.ViewModel
{
    class OrderVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Order"; }
        }

        int registerID = 4;
        string mname = "OrderVM";

        public OrderVM()
        {
            From = DateTime.Now;

            GetProducts();
            GetRegisterById(registerID);

            LoadEid();
        }

        private int _employeeID;
        public int EmployeeID
        {
            get { return _employeeID; }
            set {
                _employeeID = value;
                OnPropertyChanged("EmployeeID");
            }
        }

        private DateTime _from;
        public DateTime From
        {
            get { return _from; }
            set {
                _from = value;
                OnPropertyChanged("From");
            }
        }

        private DateTime _until;

        public DateTime Until
        {
            get { return _until; }
            set {
                _until = value;
                OnPropertyChanged("Until");
            }
        }
        
        
        
        private ObservableCollection<Products> _products;
        public ObservableCollection<Products> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
                Amount = 1;
            }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set {
                _amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private ObservableCollection<Sales> _sales;
        public ObservableCollection<Sales> Sales
        {
            get
            {
                if (_sales == null)
                    _sales = new ObservableCollection<Sales>();
                return _sales;
            }
            set
            {
                _sales = value;
                OnPropertyChanged("Sales");
            }
        }

        private Registers _register;
        public Registers Register
        {
            get
            {
                if (_register == null)
                    _register = new Registers();
                return _register;
            }
            set {
                _register = value;
                OnPropertyChanged("Register");
            }
        }

        private Sales _selectedSale;
        public Sales SelectedSale
        {
            get { return _selectedSale; }
            set
            {
                _selectedSale = value;
                OnPropertyChanged("SelectedSale");
            }
        }
        
        private decimal _total;
        public decimal Total
        {
            get { return _total; }
            set {
                _total = value;
                OnPropertyChanged("Totaal");
            }
        }

        private Customers _customer;
        public Customers Customer
        {
            get {
                return _customer;
            }
            set
            {
                _customer = value;
                OnPropertyChanged("Customer");
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
        
        private async void GetProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/Products");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                }
                else
                {
                    MakeErrorLog("Something is wrong with getting products out of db", mname, "GetProducts");
                }
            }
        }

        
        public ICommand PlusCommand
        {
            get { return new RelayCommand(Plus); }
        }

        private void Plus()
        {
            Amount++;
        }

        public ICommand MinCommand
        {
            get { return new RelayCommand(Min); }
        }

        private void Min()
        {
            if(Amount > 1)
                Amount--;
        }

        public ICommand AddToOrderCommand
        {
            get { return new RelayCommand(AddToOrder); }
        }

        private void AddToOrder()
        {
            Sales s = new Sales();
            if(SelectedProduct != null && Amount != 0 && Customer != null)
            {
                s.CustomersID = Customer.ID;
                s.RegisterID = registerID;
                s.RegisterName = Register.RegisterName;
                s.ProductID = SelectedProduct.ID;
                s.ProductName = SelectedProduct.ProductName;
                s.Amount = Amount;
                s.Price = SelectedProduct.Price;
                s.TotalPrice = Amount * SelectedProduct.Price;

                Sales.Add(s);
                CalculateTotal();
                CheckPrice();

                Amount = 1;
                SelectedProduct = null;
            }
        }

        private async void GetRegisterById(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/Register/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Register = JsonConvert.DeserializeObject<Registers>(json);
                }
                else
                {
                    MakeErrorLog("Couldn't get a register with this ID", mname, "GetRegisterByID");
                }
            }
        } 

        
        void CalculateTotal()
        {
            Total = 0;

            foreach (Sales s in Sales)
                Total += s.TotalPrice;
        }

        
        public ICommand DeleteFromOrderCommand
        {
            get { return new RelayCommand(DeleteFromOrder); }
        }

        private void DeleteFromOrder()
        {
            if (SelectedSale != null)
            {
                Sales.Remove(SelectedSale);
            }
            else
            {
                MakeErrorLog("Tried to delete something without selecting", mname, "DeleteFromOrder");
            }
                
            CalculateTotal();
        }

        
        public ICommand InsertIntoDBCommand
        {
            get { return new RelayCommand(InsertIntoDB); }
        }

        private async void InsertIntoDB()
        {
            DateTime time = DateTime.Now;
            if (Sales != null)
            {
                if (Total < Customer.Balance)
                {
                    foreach (Sales s in Sales)
                    {
                        s.Timestamp = time;
                        using (HttpClient client = new HttpClient())
                        {
                            string sale = JsonConvert.SerializeObject(s);
                            HttpResponseMessage response = await
                                client.PostAsync("http://localhost:65079/api/Sales", new StringContent(sale,
                                    Encoding.UTF8, "application/json"));
                            if (response.IsSuccessStatusCode)
                            {
                            }
                            else
                            {
                                MakeErrorLog("Couldn't add sale to db", mname, "InsertIntoDB");
                            }
                        }
                    }
                    Sales = new ObservableCollection<Sales>();
                    UpdateCustomer();
                    LogOut();
                }
                else
                {
                    MakeErrorLog("Total is bigger then balance", mname, "InsertIntoDB");
                }
            }
            else
            {
                MakeErrorLog("Empty order.", mname, "InsertIntoDB");
            }
        }

        private async void UpdateCustomer()
        {
            
            Customers c = Customer;
            c.Balance -= Total;

            using (HttpClient client = new HttpClient())
            {
                string customer = JsonConvert.SerializeObject(c);
                HttpResponseMessage response = await
                client.PutAsync("http://localhost:65079/api/Customers", new StringContent(customer,
                Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    Total = 0;
                    GetCustomer();
                }
                else
                {
                    MakeErrorLog("coulndt update customer", mname, "UpdateCustomer");
                }
            }
        }

        //E-ID
        public ICommand LoadEidCommand
        {
            get { return new RelayCommand(LoadEid); }
        }

        private void LoadEid()
        {
            BEID_ReaderSet.initSDK();

            try
            {
                BEID_ReaderSet readerSet = BEID_ReaderSet.instance();
                BEID_ReaderContext reader = readerSet.getReader();

                if (reader.isCardPresent())
                {
                    if (reader.getCardType() == BEID_CardType.BEID_CARDTYPE_EID)
                    {
                        BEID_EIDCard card = reader.getEIDCard();
                        BEID_EId doc = card.getID();

                        Customer = new Customers();
                        Customer.CustomerName = doc.getFirstName1() + " " + doc.getSurname();

                        GetCustomer();
                    }
                    else
                    {
                        MakeErrorLog("doesn't support this version of E-ID ", mname, "LoadEid");
                    }
                }
                else
                {
                    MakeErrorLog("Couldn't find an E-ID", mname, "LoadEid");
                }

                BEID_ReaderSet.releaseSDK();
            }
            catch (Exception)
            {
                MakeErrorLog("There is something wrong with reading the card", mname, "LoadEid");
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
                        MakeErrorLog("Customer needs to registrate first", mname, "GetCustomer");
                    }
                }
                else
                {
                    MakeErrorLog("Couldn't get the customer out of the db", mname, "GetCustomer");
                }
            }
        }

        private void CheckPrice()
        {
            if (Customer != null)
            {
                decimal limit = Total - Customer.Balance;
                limit = Math.Round(limit, 2);

                if (Total > Customer.Balance)
                {
                    MessageBox.Show("Total price is bigger then customers balance. There is € " + limit + " to much.", " " ,MessageBoxButton.OK, MessageBoxImage.Warning);
                    MakeErrorLog("Customer " + Customer.CustomerName + " tried to pay with € " + limit + "which wasn't on the card ", mname, "CheckPrice");
                }
            }
            else
            {
                MakeErrorLog("No E-ID was found.", mname, "CheckPrice");
            }

        }

        private void LogOut()
        {
            Until = DateTime.Now;

            Register_Employee r = new Register_Employee();
            r.RegisterID = registerID;
            r.EmployeeID = EmployeeID;
            r.Vanaf = From;
            r.Until = Until;

            InsertRegisterEmployeeIntoDB(r);
        }

        private async void InsertRegisterEmployeeIntoDB(Register_Employee r)
        {
            using (HttpClient client = new HttpClient())
            {
                string re = JsonConvert.SerializeObject(r);
                HttpResponseMessage response = await
                    client.PostAsync("http://localhost:65079/api/RegisterEmployee", new StringContent(re,
                        Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ChangePage(new LoginVM());
                }
                else
                {
                    MakeErrorLog("Couldn't add register employee to db", mname, "InsertRegisterEmployeeIntoDB");
                }
            }
        }

        
        private void MakeErrorLog(string message, string classStackTrace, string methodStackTrace)
        {
            Errorlog errorLog = new Errorlog();
            errorLog.RegisterID = registerID;
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
