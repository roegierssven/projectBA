using nmct.ba.cashlessproject.api.Models;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nmct.ba.cashlessproject.api.Controllers
{
    public class CustomersController : ApiController
    {
        public List<Customers> Get()
        {
            return CustomersDA.GetCustomers();
        }

        public Customers Get(int id)
        {
            return CustomersDA.GetCustomer(id);
        }

        public HttpResponseMessage Post(Customers c)
        {
            CustomersDA.InsertCustomer(c);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage Put(Customers c)
        {
            CustomersDA.EditCustomer(c);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
