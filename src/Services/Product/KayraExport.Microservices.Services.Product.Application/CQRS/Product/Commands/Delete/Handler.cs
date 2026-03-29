using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Commands.Delete;

public class Handler(
    ITransactionService transactionService,
    IProductRepository productRepository
) : CommandHandlerBase<Command>
{
    public override async Task<BaseResponse> Handle(Command request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(request.Id);
            if (product == null)
                return BadRequestResponse("Silinmek istenen ürün bulunamadı");

            await transactionService.BeginTransactionAsync();

            await productRepository.DeleteAsync(product);

            var affectedRows = await transactionService.SaveChangesAsync();
            if (affectedRows != 1)
            {
                await transactionService.RollbackTransactionAsync();
                return BadRequestResponse("Ürün silinemedi");
            }

            await transactionService.CommitTransactionAsync();
            return OkResponse();
        }
        catch (Exception ex)
        {
            // TODO -> log ex
            return BadRequestResponse("Beklenmeyen bir hata meydana geldi");
        }
    }
}
