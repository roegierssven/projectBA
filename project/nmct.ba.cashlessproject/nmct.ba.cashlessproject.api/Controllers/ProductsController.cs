using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products
        public List<Products> Get()
        {
            return ProductsDA.GetProducts();
        }
        public HttpResponseMessage Post(Products p)
        {
            ProductsDA.AddProduct(p);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage Put(Products p)
        {
            ProductsDA.EditProduct(p);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage Delete(int id)
        {
            ProductsDA.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        } 
    }
}