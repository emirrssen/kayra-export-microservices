using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KayraExport.Microservices.Services.Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "product",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.InsertData(
                schema: "product",
                table: "products",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Samsung Tab S10 FE", "Samsung Tablet", 22000m, 10, null },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IPhone 17 Pro", "Apple Telefon", 74899m, 5, null },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Logitech Mx Key Mini", "Logitech Klavye", 4022.74m, 18, null },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Con Sinov'dan Yarının Adamı 1 - Mustafa Kemal'i Anlamak", "Atatürk Kitabı", 249.40m, 78, null },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sony WH-CH520", "Sony Kulaklık", 2599m, 4, null },
                    { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Gri Abajur", "Abajur", 341.35m, 89, null },
                    { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dell S2721DS 27 inch Monitör", "Dell Monitör", 9865.21m, 9, null },
                    { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Mustang Güneş Gözlüğü", "Güneş Gözlüğü", 2983.89m, 16, null },
                    { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ahşap Yemek Masası", "Yemek Masası", 1799.87m, 5, null },
                    { 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Dell 32 GB Ram Laptop", "Dell Laptop", 43899.89m, 0, null }
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Products_Price_NotNegative",
                schema: "product",
                table: "products",
                sql: "\"Price\" >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Products_Price_NotNegative",
                schema: "product",
                table: "products");

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "product",
                table: "products",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "product",
                table: "products",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");
        }
    }
}
