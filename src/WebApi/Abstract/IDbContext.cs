using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Abstract
{
    public interface IDbContext
    {
        DbSet<Customer> Customers { get; set; }
        Task SaveAsync();
    }
}
