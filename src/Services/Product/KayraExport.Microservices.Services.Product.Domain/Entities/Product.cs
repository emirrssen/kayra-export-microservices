using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Entities;

namespace KayraExport.Microservices.Services.Product.Domain.Entities
{
    public class Product : BasePostgreSqlEntity
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }

        public Product(string name, string? description, decimal price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("İsmin belirtilmesi zorunludur");
            if (price < 0) throw new ArgumentOutOfRangeException("Fiyat değeri 0'dan küçük olamaz");
            if (stockQuantity < 0) throw new ArgumentOutOfRangeException($"Stok miktarı 0'dan küçük olamaz");

            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public void Update(string name, string? description, decimal price, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("İsmin belirtilmesi zorunludur");
            if (price < 0) throw new ArgumentOutOfRangeException("Fiyat değeri 0'dan küçük olamaz");
            if (stockQuantity < 0) throw new ArgumentOutOfRangeException("Stok miktarı 0'dan küçük olamaz");

            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
