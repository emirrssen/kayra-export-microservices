namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Queries.GetAll
{
    public record Response(
        long Id,
        string Name,
        string? Description,
        int StockQuantity,
        decimal Price
    );
}
