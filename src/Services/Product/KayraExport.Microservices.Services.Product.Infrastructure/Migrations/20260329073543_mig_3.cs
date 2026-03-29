using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KayraExport.Microservices.Services.Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_StockQuantity_NotNegative",
                schema: "product",
                table: "products",
                sql: "\"StockQuantity\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_StockQuantity_NotNegative",
                schema: "product",
                table: "products");
        }
    }
}
