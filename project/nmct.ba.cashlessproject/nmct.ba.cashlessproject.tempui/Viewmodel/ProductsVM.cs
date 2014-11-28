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

    }
}
