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
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.customer.ViewModel
{
    class LoginVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Login"; }
        }

        string mname = "LoginVM";

        public LoginVM()
        {
        }

        private Customers _customer = new Customers();
        public Customers Customer
        {
            get { return _customer; }
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

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        private Boolean _registrated = false;
        public Boolean Registrated
        {
            get { return _registrated; }
            set
            {
                _registrated = value;
                OnPropertyChanged("Registrated");
            }
        }

        public ICommand LoadEidCommand
        {
            get { return new RelayCommand(LoadEid); }
        }

        private void LoadEid()
        {
            BEID_ReaderSet.initSDK();
            Registrated = false;

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
                        BEID_Picture picture = card.getPicture();
                        byte[] pictureBytes = picture.getData().GetBytes();

                        Customer.CustomerName = doc.getFirstName1() + " " + doc.getSurname();
                        Customer.Address = doc.getStreet() + ", " + doc.getZipCode() + " " + doc.getMunicipality();
                        Customer.Picture = pictureBytes;
                        Customer.Balance = 0;

                        FirstName = doc.getFirstName1();
                        LastName = doc.getSurname();
                        Address = doc.getStreet();
                        City = doc.getZipCode() + " " + doc.getMunicipality();

                        CheckIfCustomerExists();
                    }
                    else
                    {
                        MakeErrorLog("Doesn't support this version", mname, "LoadEid");
                    } 
                }
                else
                {
                    MakeErrorLog("No E-ID was found", mname, "LoadEid");
                }

                BEID_ReaderSet.releaseSDK();
            }
            catch (Exception)
            {
                MakeErrorLog("Something went wrond with reading the E-ID", mname, "LoadEid");
            }

        }

        private async void CheckIfCustomerExists()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/Customers");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Customers = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);
                }
                else
                {
                    MakeErrorLog("something went wrong with getting the customers out of the db.", mname, "CheckIfCustomerExists");
                }

                Customers inDB = null;
                foreach (Customers c in Customers)
                {
                    if (c.CustomerName == Customer.CustomerName)
                        inDB = c;
                }

                if (inDB != null)
                {
                    Customer = inDB;
                    Registrated = true;
                }
                else
                {
                    AddCustomer();
                }
            }
        }

        private async void AddCustomer()
        {
            using (HttpClient client = new HttpClient())
            {
                string customer = JsonConvert.SerializeObject(Customer);
                HttpResponseMessage response = await
                    client.PostAsync("http://localhost:65079/api/Customers", new StringContent(customer,
                        Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    Registrated = true;
                }
                else
                {
                    MakeErrorLog("Something went wrong with adding the customer", mname, "AddCustomer");
                }
            }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        private void Login()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (Registrated == true)
            {
                TopupVM t = new TopupVM();
                t.Customer = Customer;
                appvm.ChangePage(t);
            }
            else
            {
                MakeErrorLog("Couldn't read card", mname, "Login");
            }
        }

        private void MakeErrorLog(string message, string classStackTrace, string methodStackTrace)
        {
            Errorlog errorLog = new Errorlog();
            errorLog.RegisterID = 1;
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
