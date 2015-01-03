using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.tempui.Viewmodel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.tempui.Viewmodel
{
    class CashregisterVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "CASH REGISTER"; }
        }

         public CashregisterVM()
        {
            GetRegisters();             
        }

         private ObservableCollection<Registers> _registers;
         public ObservableCollection<Registers> Registers
         {
             get
             {
                 if (_registers == null)
                     _registers = new ObservableCollection<Registers>();
                 return _registers;
             }
             set
             {
                 _registers = value;
                 OnPropertyChanged("Registers");
             }
         }

         private Registers _selectedRegister;

         public Registers SelectedRegister
         {
             get { return _selectedRegister; }
             set
             {
                 _selectedRegister = value;
                 OnPropertyChanged("SelectedRegister");

                 if (SelectedRegister.ID == 0)
                 {
                     GetRegisterEmployees();
                 }
                 else
                 {
                     GetRegisterEmployeesByID(SelectedRegister.ID);
                 }
             }
         }

         private ObservableCollection<Register_Employee> _registerEmployees;
         public ObservableCollection<Register_Employee> RegisterEmployees
         {
             get { return _registerEmployees; }
             set
             {
                 _registerEmployees = value;
                 OnPropertyChanged("RegisterEmployees");
             }
         }

         private List<string> _registerNames;

         public List<string> RegisterNames
         {
             get { return _registerNames; }
             set { _registerNames = value; }
         }


         
         private async void GetRegisters()
         {
             Registers allRegisters = new Registers();
             allRegisters.ID = 0;
             allRegisters.RegisterName = "All Registers";
             allRegisters.Device = " ";

             using (HttpClient client = new HttpClient())
             {
                 HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/register");
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     Registers = JsonConvert.DeserializeObject<ObservableCollection<Registers>>(json);
                     Registers.Add(allRegisters);
                 }
             }
         }

         private async void GetRegisterEmployees()
         {
             using (HttpClient client = new HttpClient())
             {
                 HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/registeremployee");
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     RegisterEmployees = JsonConvert.DeserializeObject<ObservableCollection<Register_Employee>>(json);
                 }
             }
         }

         private async void GetRegisterEmployeesByID(int id)
         {
             using (HttpClient client = new HttpClient())
             {
                 HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/registeremployee/" + id);
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     RegisterEmployees = JsonConvert.DeserializeObject<ObservableCollection<Register_Employee>>(json);
                 }
             }
         }
    }
}
