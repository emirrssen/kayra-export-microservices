using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KayraExport.Microservices.Services.Product.Infrastructure.EntityFrameworkCore
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public DbSet<Domain.Entities.Product> Products { get; set; }
    }
}
