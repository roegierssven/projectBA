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
    class ProductsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "PRODUCTS"; }
        }

        
        
        public ProductsVM() 
        {
        
        GetProducts();

        }

        private async void GetProducts()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await
               client.GetAsync("http://localhost:65079/api/products");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                }
            }
        }
        private ObservableCollection<Products> _products;
        public ObservableCollection<Products> Products
        {
            get { return _products; }
            set { _products = value; OnPropertyChanged("Products"); }
        }
        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct= value; OnPropertyChanged("SelectedProduct");
            if (SelectedProduct != null)
            {
                Id = SelectedProduct.ID;
                ProductName = SelectedProduct.ProductName;
                Price = SelectedProduct.Price.ToString();
            }
            }
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value;
            OnPropertyChanged("ProductName");
            }
        }

        private string _price;

        public string Price
        {
            get { return _price; }
            set { _price = value;
            OnPropertyChanged("Price");
            }
        }
        
        

        public ICommand AddProductCommand
{
 get { return new RelayCommand(AddProduct); }
}
public ICommand DeleteProductCommand
{
 get { return new RelayCommand(DeleteProduct); }
}
public ICommand SaveProductCommand
{
 get { return new RelayCommand(SaveProduct); }
}
public async void AddProduct()
{
    Products newProduct = new Products();
    newProduct.ProductName = ProductName;
    newProduct.Price = Convert.ToDecimal(Price);
    
    int t = 0;

    foreach (Products p in Products)
    {
        if (p.ProductName == newProduct.ProductName)
        {
            t++;
        }

        
    }

    if (t == 0)
    {
        using (HttpClient client = new HttpClient())
        {
            string product = JsonConvert.SerializeObject(newProduct);
            HttpResponseMessage response = await
           client.PostAsync("http://localhost:65079/api/products", new StringContent(product,
           Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {

                GetProducts();
                Id = 0;
                ProductName = "";
                Price = "";
            }
        }
    }
    
}
public async void DeleteProduct()
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await
       client.DeleteAsync("http://localhost:65079/api/products/" + SelectedProduct.ID);
        if (response.IsSuccessStatusCode)
        {
            GetProducts();
            ProductName = "";
            Price = "";
        }
    }
}
public async void SaveProduct()
{
    Products p = new Products();
    p.ID = Id;
    p.ProductName = ProductName;
    p.Price = Convert.ToDecimal(Price);
    using (HttpClient client = new HttpClient())
    {
        string product = JsonConvert.SerializeObject(p);
        HttpResponseMessage response = await
       client.PutAsync("http://localhost:65079/api/products", new StringContent(product,
       Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            GetProducts();
        }
    }
    
}

    }
}
