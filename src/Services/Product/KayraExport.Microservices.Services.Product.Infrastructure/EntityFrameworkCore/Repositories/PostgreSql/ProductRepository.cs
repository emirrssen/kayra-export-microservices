using KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace KayraExport.Microservices.Services.Product.Infrastructure.EntityFrameworkCore.Repositories.PostgreSql
{
    public class ProductRepository : IProductRepository
    {
        public DbSet<Domain.Entities.Product> Products { get; set; }
        protected ProductDbContext Context { get; private set; }

        public ProductRepository(ProductDbContext context)
        {
            Context = context;
            Products = context.Set<Domain.Entities.Product>();
        }
    }
}
