using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Update
{
    public class Command : CommandBase
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
