using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Abstract;
using WebApi.Models;

namespace WebApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDbContext dbContext;

        public CustomerService(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<long> CreateCustomerAsync(Customer customer)
        {
            Customer newCustomer = (await dbContext.Customers.AddAsync(customer)).Entity;
            await dbContext.SaveAsync();
            return newCustomer.Id;
        }

        public async Task<Customer> GetCustomerAsync(long id)
        {
            return await dbContext.Customers.FirstOrDefaultAsync(cust => cust.Id.Equals(id));
        }
    }
}
