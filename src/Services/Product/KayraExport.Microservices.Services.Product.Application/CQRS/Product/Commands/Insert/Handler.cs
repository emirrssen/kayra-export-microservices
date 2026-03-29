using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Insert;

public class Handler(
    ITransactionService transactionService,
    IProductRepository productRepository
) : CommandHandlerBase<Command>
{
    public override async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
    {
        try
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
            return CreatedResponse();
        }
        catch (Exception ex)
        {
            // TODO -> log ex
            return BadRequestResponse("Beklenmeyen bir hata meydana geldi");
        }
    }
}
