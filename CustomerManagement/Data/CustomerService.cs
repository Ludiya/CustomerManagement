using CustomerManagement.Data.Entities;
using CustomerManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Data
{
    public class CustomerService
    {
        private readonly IRepository<Customer> _customer;

        public CustomerService(IRepository<Customer> customer)
        {
            _customer = customer;
        }
        public Customer GetCustomerById(int Id)
        {
            return _customer.GetAll().Where(x => x.CustomerId == Id).FirstOrDefault();
        }
        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                return _customer.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Customer CustomerDetails(int id)
        {
            return _customer.GetById(id);
        }
        public Customer CustomerLogin(string UserName, string Password)
        {
            return _customer.GetAll().Where(c => c.UserName == UserName && c.Password == Password).FirstOrDefault();
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            return await _customer.Create(customer);
        } 
        public bool DeleteCustomer(int id)
        {
            try
            {
                var customer = this.GetCustomerById(id);
                _customer.Delete(customer);
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }  
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                var customers = _customer.GetAll().Where(c => c.CustomerId == customer.CustomerId).ToList();
                foreach (var newInfo in customers)
                {
                    _customer.Update(newInfo);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
