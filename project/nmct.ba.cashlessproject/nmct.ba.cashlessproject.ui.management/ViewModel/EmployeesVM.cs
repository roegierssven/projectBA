using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.ui.management.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace nmct.ba.cashlessproject.ui.management.ViewModel
{
    class EmployeesVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "EMPLOYEES"; }
        }

         
        public EmployeesVM() 
        {
        
        GetEmployees();

        }

        private async void GetEmployees()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
               client.GetAsync("http://localhost:65079/api/employees");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                }
            }
        }
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("Employees"); }
        }
        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee= value; OnPropertyChanged("SelectedEmployee");
            if (SelectedEmployee != null)
            {
                Id = SelectedEmployee.ID;
                EmployeeName = SelectedEmployee.EmployeeName;
                Address = SelectedEmployee.Address;
                Email = SelectedEmployee.Email;
                Phone = SelectedEmployee.Phone;
                
            }
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        

        private string _employeeName;

        public string EmployeeName
        {
            get { return _employeeName; }
            set { _employeeName = value;
            OnPropertyChanged("EmployeeName");
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

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value;
            OnPropertyChanged("Email");
            }
        }
        
        

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value;
            OnPropertyChanged("Phone");
            }
        }
        
        

        public ICommand AddEmployeeCommand
{
 get { return new RelayCommand(AddEmployee); }
}
public ICommand DeleteEmployeeCommand
{
 get { return new RelayCommand(DeleteEmployee); }
}
public ICommand SaveEmployeeCommand
{
 get { return new RelayCommand(SaveEmployee); }
}
public async void AddEmployee()
{
    Employee newEmployee = new Employee();
    newEmployee.EmployeeName = EmployeeName;
    newEmployee.Address = Address;
    newEmployee.Email = Email;
    newEmployee.Phone = Phone;
    
    int t = 0;

    foreach (Employee e in Employees)
    {
        if (e.EmployeeName == newEmployee.EmployeeName)
        {
            t++;
        }

        
    }

    if (t == 0)
    {
        using (HttpClient client = new HttpClient())
        {
            string employee = JsonConvert.SerializeObject(newEmployee);
            HttpResponseMessage response = await
           client.PostAsync("http://localhost:65079/api/employees/", new StringContent(employee,
           Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {

                GetEmployees();
                Id = 0;
                EmployeeName = "";
                Address = "";
                Email = "";
                Phone = "";
            }
        }
    }
    
}
public async void DeleteEmployee()
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await
       client.DeleteAsync("http://localhost:65079/api/employees/" + SelectedEmployee.ID);
        if (response.IsSuccessStatusCode)
        {
            GetEmployees();
            EmployeeName = "";
            Address = "";
            Email = "";
            Phone = "";
        }
    }
}
public async void SaveEmployee()
{
    Employee e = new Employee();
    e.ID = Id;
    e.EmployeeName = EmployeeName;
    e.Address = Address;
    e.Email = Email;
    e.Phone = Phone;
    
    using (HttpClient client = new HttpClient())
    {
        string employee = JsonConvert.SerializeObject(e);
        HttpResponseMessage response = await
       client.PutAsync("http://localhost:65079/api/employees/", new StringContent(employee,
       Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            GetEmployees();
        }
    }
    
}
    }
}
