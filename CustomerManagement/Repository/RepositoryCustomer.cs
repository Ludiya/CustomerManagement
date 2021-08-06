using CustomerManagement.Data;
using CustomerManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class RepositoryCustomer : IRepository<Customer>
    {
        private CustomerDbContext _dbContext;
        public RepositoryCustomer(CustomerDbContext DbContext)
        {
            _dbContext = DbContext;
        }
        public async Task<Customer> Create(Customer _object)
        {
            var obj = await _dbContext.customers.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Customer _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            try
            {
                return _dbContext.customers.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Customer GetById(int Id)
        {
            return _dbContext.customers.Where(c => c.CustomerId == Id).FirstOrDefault();
        }

        public void Update(Customer _customer)
        {
            _dbContext.customers.Update(_customer);
            _dbContext.SaveChanges();
        }
    }
}
