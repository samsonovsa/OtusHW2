using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstract;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }

        [HttpGet("{id:long}")]   
        public async Task<Customer> GetCustomerAsync([FromRoute] long id)
        {
            return await service.GetCustomerAsync(id);
        }

        [HttpPost("create")]   
        public async Task<long> CreateCustomerAsync([FromBody] Customer customer)
        {
            return await service.CreateCustomerAsync(customer);
        }
    }
}