using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Helpers;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Product.Domain.Consts;
using KayraExport.Microservices.Services.Product.Domain.Events;
using Microsoft.Extensions.Caching.Distributed;
using Rebus.Bus;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Insert;

public class Handler(
    ITransactionService transactionService,
    IProductRepository productRepository,
    IBus bus,
    IDistributedCache cache
) : CommandHandlerBase<Command>
{
    public override async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
    {
        await transactionService.BeginTransactionAsync();

        Domain.Entities.Product product = new(
            request.Name,
            request.Description,
            request.Price,
            request.StockQuantity
        );

        await productRepository.InsertAsync(product);

        var affectedRows = await transactionService.SaveChangesAsync();
        if (affectedRows != 1)
        {
            await transactionService.RollbackTransactionAsync();
            return BadRequestResponse("Ürün kaydedilemedi");
        }

        await transactionService.CommitTransactionAsync();

        await bus.Send(new ProductCreatedEvent
        {
            CreatedAt = DateTimeHelper.GetNowByTurkiyeTimeZone(),
            CreatedProductId = product.Id
        });

        await cache.RemoveAsync(CacheKeyConst.ProductsCacheKey, cancellationToken);

        return CreatedResponse();
    }
}
