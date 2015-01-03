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
    public class EmployeesController : ApiController
    {

        public List<Employee> Get()
        {
            return EmployeeDA.GetEmployees();
        }

        public Employee Get(int id)
        {
            return EmployeeDA.GetEmployee(id);
        }

        public HttpResponseMessage Post(Employee e)
        {
           EmployeeDA.AddEmployee(e);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage Put(Employee e)
        {
            EmployeeDA.EditEmployee(e);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        public HttpResponseMessage Delete(int id)
        {
            EmployeeDA.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        } 
    }
}
