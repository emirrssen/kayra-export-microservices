using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KayraExport.Microservices.Services.Product.Infrastructure.EntityFrameworkCore.Seed;

public class ProductSeed : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
    {
        builder.HasData(new List<Domain.Entities.Product>
        {
            CreateProduct(1, "Samsung Tablet", "Samsung Tab S10 FE", 10, 22000m),
            CreateProduct(2, "Apple Telefon", "IPhone 17 Pro", 5, 74899m),
            CreateProduct(3, "Logitech Klavye", "Logitech Mx Key Mini", 18, 4022.74m),
            CreateProduct(4, "Atatürk Kitabı", "Con Sinov'dan Yarının Adamı 1 - Mustafa Kemal'i Anlamak", 78, 249.40m),
            CreateProduct(5, "Sony Kulaklık", "Sony WH-CH520", 4, 2599m),
            CreateProduct(6, "Abajur", "Gri Abajur", 89, 341.35m),
            CreateProduct(7, "Dell Monitör", "Dell S2721DS 27 inch Monitör", 9, 9865.21m),
            CreateProduct(8, "Güneş Gözlüğü", "Mustang Güneş Gözlüğü", 16, 2983.89m),
            CreateProduct(9, "Yemek Masası", "Ahşap Yemek Masası", 5, 1799.87m),
            CreateProduct(10, "Dell Laptop", "Dell 32 GB Ram Laptop", 0, 43899.89m)
        });
    }

    private Domain.Entities.Product CreateProduct(long id, string name, string? description, int stockQuantity, decimal price)
    {
        Domain.Entities.Product product = new(name, description, price, stockQuantity);
        product.Id = id;

        return product;
    }
}
