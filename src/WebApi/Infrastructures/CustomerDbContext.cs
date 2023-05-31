using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebApi.Abstract;
using WebApi.Models;

namespace WebApi.Infrastructures
{
    public class CustomerDbContext: DbContext, IDbContext
    {
        private const string ConnectionStringName = "Default";
        private readonly string connectionString;

        public CustomerDbContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString(ConnectionStringName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        public async Task SaveAsync()
        {
            await this.SaveChangesAsync();
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
