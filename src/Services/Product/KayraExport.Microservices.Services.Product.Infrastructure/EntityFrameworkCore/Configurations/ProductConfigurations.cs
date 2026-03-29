using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KayraExport.Microservices.Services.Product.Infrastructure.EntityFrameworkCore.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
        {
            builder.ToTable("products", SchemasConst.Product, x =>
            {
                x.HasCheckConstraint("CK_Products_Price_NotNegative", @"""Price"" >= 0");
                x.HasCheckConstraint("CK_Products_StockQuantity_NotNegative", @"""StockQuantity"" >= 0");
            });

            builder.Property(x => x.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
        }
    }
}
