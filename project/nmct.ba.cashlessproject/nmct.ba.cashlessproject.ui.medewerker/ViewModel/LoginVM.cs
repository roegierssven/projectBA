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

namespace nmct.ba.cashlessproject.ui.medewerker.ViewModel
{
    class LoginVM: ObservableObject, IPage
    {
         

        public string Name
        {
            get { return "Login"; }
        }

        int registerID = 3;
        string mname = "LoginVM";

        public LoginVM()
        {
            GetEmployees();
        }

        private string _employeeID;
        public string EmployeeID
        {
            get { return _employeeID; }
            set {
                _employeeID = value;
                OnPropertyChanged("EmployeeID");
            }
        }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }

        private Boolean _isID;

        public Boolean IsID
        {
            get { return _isID; }
            set
            {
                _isID = value;
                OnPropertyChanged("IsID");
            }
        }
        

        #region "Buttons"
        //0
        public ICommand Addg0Command
        {
            get { return new RelayCommand(Add0); }
        }
        private void Add0()
        {
            EmployeeID = EmployeeID + "0";
        }

        //1
        public ICommand Add1Command
        {
            get { return new RelayCommand(Add1); }
        }
        private void Add1()
        {
            EmployeeID = EmployeeID + "1";
        }

        //2
        public ICommand Add2Command
        {
            get { return new RelayCommand(Add2); }
        }
        private void Add2()
        {
            EmployeeID = EmployeeID + "2";
        }

        //3
        public ICommand Add3Command
        {
            get { return new RelayCommand(Add3); }
        }
        private void Add3()
        {
            EmployeeID = EmployeeID + "3";
        }

        //4
        public ICommand Add4Command
        {
            get { return new RelayCommand(Add4); }
        }
        private void Add4()
        {
            EmployeeID = EmployeeID + "4";
        }

        //5
        public ICommand Add5Command
        {
            get { return new RelayCommand(Add5); }
        }
        private void Add5()
        {
            EmployeeID = EmployeeID + "5";
        }

        //6
        public ICommand Add6Command
        {
            get { return new RelayCommand(Add6); }
        }
        private void Add6()
        {
            EmployeeID = EmployeeID + "6";
        }

        //7
        public ICommand Add7Command
        {
            get { return new RelayCommand(Add7); }
        }
        private void Add7()
        {
            EmployeeID = EmployeeID + "7";
        }

        //8
        public ICommand Add8Command
        {
            get { return new RelayCommand(Add8); }
        }
        private void Add8()
        {
            EmployeeID = EmployeeID + "8";
        }

        //9
        public ICommand Add9Command
        {
            get { return new RelayCommand(Add9); }
        }
        private void Add9()
        {
            EmployeeID = EmployeeID + "9";
        }

        
        public ICommand DeleteCommand
        {
            get { return new RelayCommand(Delete); }
        }
        private void Delete()
        {
            EmployeeID = "";
        }
        #endregion

        
        private async void GetEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/Employees");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
                else
                {
                    MakeErrorLog("Something went wrong with getting employees out of db", mname, "GetEmployees");
                }
            }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }
        private void Login()
        {
            CheckEmployeeID();

            if (IsID == true)
            {
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                OrderVM order = new OrderVM();
                order.EmployeeID = Convert.ToInt32(EmployeeID);

                appvm.ChangePage(order);
            }
            else
            {
                MakeErrorLog("Login with wrong ID: " + EmployeeID, mname, "Login");
            }
        }

        private void CheckEmployeeID()
        {
            foreach (Employee e in Employees)
            {
                if (e.ID == Convert.ToInt32(EmployeeID))
                    IsID = true;
            }
        }

        //Errorlog
        private void MakeErrorLog(string message, string classStackTrace, string methodStackTrace)
        {
            Errorlog errorLog = new Errorlog();
            errorLog.RegisterID = registerID;
            errorLog.Timestamp = DateTimeToUnixTimeStamp(DateTime.Now);
            errorLog.Message = message;
            errorLog.StackTrace = "Class: " + classStackTrace + " Method: " + methodStackTrace;

            AddErrorlog(errorLog);
        }

        //Errorlog DB
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
