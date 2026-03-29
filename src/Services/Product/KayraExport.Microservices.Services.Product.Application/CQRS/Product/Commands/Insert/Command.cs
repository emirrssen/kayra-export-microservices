using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Insert
{
    public class Command : CommandBase
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
