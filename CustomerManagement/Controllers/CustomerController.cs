using CustomerManagement.Data;
using CustomerManagement.Data.Entities;
using CustomerManagement.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        private readonly IRepository<Customer> _customer;

        public CustomerController(IRepository<Customer> customer, CustomerService customerService)
        {
            _customerService = customerService;
            _customer = customer;
        }
        [HttpPost]
        public async Task<Object> Post([FromBody] Customer customer)
        {
            try
            {
                await _customerService.AddCustomer(customer);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                _customerService.DeleteCustomer(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPut]
        public bool Put(Customer customer)
        {
            try
            {
                _customerService.UpdateCustomer(customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost("Login")]
        public Object CustomerLogin(string userName, string Password)
        {
            var customer = _customerService.CustomerLogin(userName, Password);
            var customerList = JsonConvert.SerializeObject(customer, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            return customerList;
        }
        [HttpGet]
        public Object GetAllCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            return customers;
        }
        [HttpGet("{id}")]
        public Object CustomerDetails(int id)
        {
            var customers = _customerService.GetCustomerById(id);
            return customers;
        }
    }
}
