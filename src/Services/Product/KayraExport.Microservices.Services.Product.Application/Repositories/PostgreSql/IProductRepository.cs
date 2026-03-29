namespace KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql
{
    public interface IProductRepository
    {
        Task<Domain.Entities.Product?> GetByIdAsync(long id);
        Task InsertAsync(Domain.Entities.Product product);
        Task UpdateAsync(Domain.Entities.Product product);
        Task DeleteAsync(Domain.Entities.Product product);
        Task<IEnumerable<Domain.Entities.Product>> GetAllAsync();
    }
}
