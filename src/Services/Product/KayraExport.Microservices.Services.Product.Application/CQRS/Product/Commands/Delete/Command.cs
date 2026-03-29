using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Delete
{
    public class Command : CommandBase
    {
        public long Id { get; set; }
    }
}
