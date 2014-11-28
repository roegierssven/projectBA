using nmct.ba.cashlessproject.model;
using nmct.ba.cashlessproject.tempui.Viewmodel;
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


namespace nmct.ba.cashlessproject.tempui.Viewmodel
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
            set { _selectedProduct= value; OnPropertyChanged("SelectedProduct"); }
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
    Products.Add(newProduct);
    using (HttpClient client = new HttpClient())
    {
        string product = JsonConvert.SerializeObject(newProduct);
        HttpResponseMessage response = await
       client.PostAsync("http://localhost:65079/api/product", new StringContent(product,
       Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            
            SelectedProduct = newProduct;
        }
    }
}
public async void DeleteProduct()
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await
       client.DeleteAsync("http://localhost:65079/api/products/1");
        if (response.IsSuccessStatusCode)
        {
            Products.Remove(SelectedProduct);
        }
    }
}
public async void SaveProduct()
{
    using (HttpClient client = new HttpClient())
    {
        string product = JsonConvert.SerializeObject(SelectedProduct);
        HttpResponseMessage response = await
       client.PutAsync("http://localhost:65079/api/products", new StringContent(product,
       Encoding.UTF8, "application/json"));
    }
}

    }
}
