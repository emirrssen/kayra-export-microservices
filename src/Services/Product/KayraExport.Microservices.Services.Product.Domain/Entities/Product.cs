using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Entities;

namespace KayraExport.Microservices.Services.Product.Domain.Entities
{
    public class Product : BasePostgreSqlEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
