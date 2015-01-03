using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.ui.management.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.management.ViewModel
{
    class StatisticsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "STATISTICS"; }
        }

         public StatisticsVM()
        {
            GetRegisters();
            GetProducts();
            GetSales();
        }

         private ObservableCollection<Registers> _registers;
         public ObservableCollection<Registers> Registers
         {
             get { return _registers; }
             set
             {
                 _registers = value;
                 OnPropertyChanged("Registers");
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

         private Registers _selectedRegister;
         public Registers SelectedRegister
         {
             get { return _selectedRegister; }
             set
             {
                 _selectedRegister = value;
                 OnPropertyChanged("SelectedRegister");
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
             }
         }

         private ObservableCollection<Sales> _sales;
         public ObservableCollection<Sales> Sales
         {
             get { return _sales; }
             set
             {
                 _sales = value;
                 OnPropertyChanged("Sales");
                 ShowSales = Sales;
             }
         }

         private ObservableCollection<Sales> _showSales;
         public ObservableCollection<Sales> ShowSales
         {
             get { return _showSales; }
             set
             {
                 _showSales = value;
                 OnPropertyChanged("ShowSales");
                 CalculateTotal();
             }
         }

         private DateTime _from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
         public DateTime From
         {
             get { return _from; }
             set
             {
                 _from = value;
                 OnPropertyChanged("From");
                 FromChanged = true;
             }
         }

         private DateTime _until = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
         public DateTime Until
         {
             get { return _until; }
             set
             {
                 _until = value;
                 OnPropertyChanged("Until");
                 UntilChanged = true;
             }
         }

         private decimal _total;
         public decimal Total
         {
             get { return _total; }
             set
             {
                 _total = value;
                 OnPropertyChanged("Total");
             }
         }

         private Boolean _fromChanged = false;
         public Boolean FromChanged
         {
             get { return _fromChanged; }
             set
             {
                 _fromChanged = value;
                 OnPropertyChanged("FromChanged");
             }
         }

         private Boolean _untilChanged = false;
         public Boolean UntilChanged
         {
             get { return _untilChanged; }
             set
             {
                 _untilChanged = value;
                 OnPropertyChanged("UntilChanged");
             }
         }


         
         private async void GetRegisters()
         {
             Registers r = new Registers();
             r.ID = 0;
             r.RegisterName = "All Registers";
             using (HttpClient client = new HttpClient())
             {
                 HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/register");
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     Registers = JsonConvert.DeserializeObject<ObservableCollection<Registers>>(json);
                     Registers.Add(r);
                 }
             }
         }

         
         private async void GetProducts()
         {
             Products p = new Products();
             p.ID = 0;
             p.ProductName = "All Products";

             using (HttpClient client = new HttpClient())
             {
                 HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/products");
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                     Products.Add(p);
                 }
             }
         }

         
         private async void GetSales()
         {
             using (HttpClient client = new HttpClient())
             {
                 HttpResponseMessage response = await client.GetAsync("http://localhost:65079/api/Sale");
                 if (response.IsSuccessStatusCode)
                 {
                     string json = await response.Content.ReadAsStringAsync();
                     Sales = JsonConvert.DeserializeObject<ObservableCollection<Sales>>(json);
                 }
             }
         }

        
         public ICommand SearchStatsCommand
         {
             get { return new RelayCommand(SearchStats); }
         }

         private void SearchStats()
         {
             ShowSales = new ObservableCollection<Sales>();
             DateTime standardDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

             if (FromChanged == true && UntilChanged == true && SelectedRegister != null && SelectedRegister.ID > 0 && SelectedProduct != null && SelectedProduct.ID > 0)
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.Timestamp.Ticks > From.Ticks && s.Timestamp.Ticks < Until.Ticks && s.RegisterID == SelectedRegister.ID && s.ProductID == SelectedProduct.ID)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == true && UntilChanged == true && SelectedRegister != null && SelectedRegister.ID > 0 && (SelectedProduct == null || SelectedProduct.ID == 0))
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.Timestamp.Ticks > From.Ticks && s.Timestamp.Ticks < Until.Ticks && s.RegisterID == SelectedRegister.ID)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == true && UntilChanged == true && (SelectedRegister == null || SelectedRegister.ID == 0) && SelectedProduct != null && SelectedProduct.ID > 0)
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.Timestamp.Ticks > From.Ticks && s.Timestamp.Ticks < Until.Ticks && s.ProductID == SelectedProduct.ID)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == false && UntilChanged == false && SelectedRegister != null && SelectedRegister.ID > 0 && SelectedProduct != null && SelectedProduct.ID > 0)
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.RegisterID == SelectedRegister.ID && s.ProductID == SelectedProduct.ID)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == true && UntilChanged == true && (SelectedRegister == null || SelectedRegister.ID == 0) && (SelectedProduct == null || SelectedProduct.ID == 0))
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.Timestamp.Ticks > From.Ticks && s.Timestamp.Ticks < Until.Ticks)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == false && UntilChanged == false && SelectedRegister != null && SelectedRegister.ID > 0 && (SelectedProduct == null || SelectedProduct.ID == 0))
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.RegisterID == SelectedRegister.ID)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == false && UntilChanged == false && (SelectedRegister == null || SelectedRegister.ID == 0) && SelectedProduct != null && SelectedProduct.ID > 0)
             {
                 foreach (Sales s in Sales)
                 {
                     if (s.ProductID == SelectedProduct.ID)
                         ShowSales.Add(s);
                 }
             }

             if (FromChanged == false && UntilChanged == false && (SelectedRegister == null || SelectedRegister.ID == 0) && (SelectedProduct == null || SelectedProduct.ID == 0))
             {
                 ShowSales = Sales;
             }

             CalculateTotal();
         }

         
         void CalculateTotal()
         {
             Total = 0;

             foreach (Sales s in ShowSales)
                 Total += s.TotalPrice;
         }

         public ICommand ExportToExcelCommand
         {
             get { return new RelayCommand(ExportToExcel); }
         }

         private void ExportToExcel()
         {
             DateTime time = DateTime.Now;
             string t = time.ToString("dd-MM-yy.hhumm");

             string fromDate;
             string untilDate;
             string rName;
             string pName;

             if (FromChanged == true)
             {
                 fromDate = From.ToString("dd-MM-yy");
             }
             else
             {
                 fromDate = "-";
             }

             if (UntilChanged == true)
             {
                 untilDate = Until.ToString("dd-MM-yy");
             }
             else
             {
                 untilDate = "-";
             }

             if (SelectedRegister != null && SelectedRegister.ID > 0)
             {
                 rName = SelectedRegister.RegisterName;
             }
             else
             {
                 rName = "-";
             }

             if (SelectedProduct != null && SelectedProduct.ID > 0)
             {
                 pName = SelectedProduct.ProductName;
             }
             else
             {
                 pName = "-";
             }

             
             SpreadsheetDocument doc = SpreadsheetDocument.Create(t + ".xlsx", SpreadsheetDocumentType.Workbook);

             WorkbookPart wbp = doc.AddWorkbookPart();
             wbp.Workbook = new Workbook();

             WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
             SheetData data = new SheetData();
             wsp.Worksheet = new Worksheet(data);

             Sheets sheets = doc.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

             
             Sheet sheet = new Sheet()
             {
                 Id = doc.WorkbookPart.GetIdOfPart(wsp),
                 SheetId = 1,
                 Name = "Statistics"
             };
             sheets.Append(sheet);

             
             Row searchdata = new Row() { RowIndex = 1 };
             Cell from = new Cell() { CellReference = "A1", DataType = CellValues.String, CellValue = new CellValue("Until:") };
             Cell fromsd = new Cell() { CellReference = "B1", DataType = CellValues.String, CellValue = new CellValue(fromDate) };
             Cell until = new Cell() { CellReference = "C1", DataType = CellValues.String, CellValue = new CellValue("Until:") };
             Cell untilsd = new Cell() { CellReference = "D1", DataType = CellValues.String, CellValue = new CellValue(untilDate) };
             Cell register = new Cell() { CellReference = "E1", DataType = CellValues.String, CellValue = new CellValue("Register:") };
             Cell registersd = new Cell() { CellReference = "F1", DataType = CellValues.String, CellValue = new CellValue(rName) };
             Cell product = new Cell() { CellReference = "G1", DataType = CellValues.String, CellValue = new CellValue("Product:") };
             Cell productsd = new Cell() { CellReference = "H1", DataType = CellValues.String, CellValue = new CellValue(pName) };

             searchdata.Append(from, fromsd, until, untilsd, register, registersd, product, productsd);
             data.Append(searchdata);

             
             Row header = new Row() { RowIndex = 3 };
             Cell registerh = new Cell() { CellReference = "A3", DataType = CellValues.String, CellValue = new CellValue("Register") };
             Cell producth = new Cell() { CellReference = "B3", DataType = CellValues.String, CellValue = new CellValue("Product") };
             Cell timestamph = new Cell() { CellReference = "C3", DataType = CellValues.String, CellValue = new CellValue("Timestamp") };
             Cell amounth = new Cell() { CellReference = "D3", DataType = CellValues.String, CellValue = new CellValue("Amount") };
             Cell priceh = new Cell() { CellReference = "E3", DataType = CellValues.String, CellValue = new CellValue("Price") };
             Cell totalpriceh = new Cell() { CellReference = "F3", DataType = CellValues.String, CellValue = new CellValue("Total Price") };

             header.Append(registerh, producth, timestamph, amounth, priceh, totalpriceh);
             data.Append(header);

             
             int i;
             int c = ShowSales.Count;

             for (i = 0; i < c; i++)
             {
                 int ii = i + 4;
                 Row sale = new Row() { RowIndex = Convert.ToUInt32(ii) };
                 Cell registerName = new Cell() { CellReference = "A" + ii, DataType = CellValues.String, CellValue = new CellValue(ShowSales[i].RegisterName) };
                 Cell productName = new Cell() { CellReference = "B" + ii, DataType = CellValues.String, CellValue = new CellValue(ShowSales[i].ProductName) };
                 Cell timestamp = new Cell() { CellReference = "C" + ii, DataType = CellValues.String, CellValue = new CellValue(ShowSales[i].Timestamp.ToString()) };
                 Cell amount = new Cell() { CellReference = "D" + ii, DataType = CellValues.String, CellValue = new CellValue(ShowSales[i].Amount.ToString()) };
                 Cell price = new Cell() { CellReference = "E" + ii, DataType = CellValues.String, CellValue = new CellValue(ShowSales[i].Price.ToString()) };
                 Cell totalprice = new Cell() { CellReference = "F" + ii, DataType = CellValues.String, CellValue = new CellValue(ShowSales[i].TotalPrice.ToString()) };

                 sale.Append(registerName, productName, timestamp, amount, price, totalprice);
                 data.Append(sale);
             }

             
             wbp.Workbook.Save();
             doc.Close();
         }

    }
}
