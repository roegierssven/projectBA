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
    public class RegisterEmployeeController : ApiController
    {
        public List<Register_Employee> Get()
        {
            return Register_EmployeeDA.GetRegister_Employees();
        }

        public List<Register_Employee> Get(int id)
        {
            return Register_EmployeeDA.GetRegister_Employee(id);
        }

        public HttpResponseMessage Post(Register_Employee r)
        {
            Register_EmployeeDA.AddRegisterEmployee(r);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
