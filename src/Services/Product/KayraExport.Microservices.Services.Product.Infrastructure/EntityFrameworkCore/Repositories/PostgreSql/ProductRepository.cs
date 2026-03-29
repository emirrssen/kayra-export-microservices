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

        public async Task<Domain.Entities.Product?> GetByIdAsync(long id)
            => await Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task InsertAsync(Domain.Entities.Product product) => await Products.AddAsync(product);
        public async Task UpdateAsync(Domain.Entities.Product product) => await Task.FromResult(Products.Update(product));
        public async Task DeleteAsync(Domain.Entities.Product product) => await Task.FromResult(Products.Remove(product));

        public async Task<IEnumerable<Domain.Entities.Product>> GetAllAsync()
        {
            var query = Products.AsQueryable();
            query.AsNoTrackingWithIdentityResolution();

            return await query.Where(x => x.DeletedAt == null).ToListAsync();
        }
    }
}
