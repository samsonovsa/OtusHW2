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
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] long id)
        {
            Customer customer = await service.GetCustomerAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost("create")]   
        public async Task<ActionResult> CreateCustomerAsync([FromBody] Customer customer)
        {
            long newCustomerId = await service.CreateCustomerAsync(customer);
            if(newCustomerId == 0)
            {
                return Conflict();
            }

            return Ok(newCustomerId);
        }
    }
}