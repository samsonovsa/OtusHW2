using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Abstract
{
    public interface ICustomerService
    {
       Task<Customer> GetCustomerAsync(long id);
       Task<long> CreateCustomerAsync(Customer customer);
    }
}
