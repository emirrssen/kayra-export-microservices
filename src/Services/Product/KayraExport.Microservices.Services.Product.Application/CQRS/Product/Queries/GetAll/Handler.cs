using KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Query;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Helpers;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using KayraExport.Microservices.Services.Product.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Product.Domain.Consts;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace KayraExport.Microservices.Services.Product.Application.CQRS.Product.Queries.GetAll;

public class Handler(
    IDistributedCache cache,
    IProductRepository productRepository
) : QueryHandlerBase<Query, List<Response>>
{
    public override async Task<DataResponse<List<Response>>> Handle(Query request, CancellationToken cancellationToken)
    {
        var cachedData = await cache.GetStringAsync(CacheKeyConst.ProductsCacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cachedData))
        {
            var deserilazedProducts = JsonSerializer.Deserialize<List<Domain.Entities.Product>>(cachedData);
            if (deserilazedProducts == null || !deserilazedProducts.Any())
                return NoContentResponse("Sistemde tanımlı ürün bilgisi bulunamadı");

            return OkResponse(MapProducts(deserilazedProducts));
        }

        var products = await productRepository.GetAllAsync();

        var serializedProducts = JsonSerializer.Serialize(products);
        await cache.SetStringAsync(CacheKeyConst.ProductsCacheKey, serializedProducts, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeHelper.GetNowByTurkiyeTimeZone().AddMinutes(10)
        }, cancellationToken);

        return OkResponse(MapProducts(products));
    }

    private List<Response> MapProducts(IEnumerable<Domain.Entities.Product> products)
    {
        return products
            .Select(x => new Response(
                x.Id,
                x.Name,
                x.Description,
                x.StockQuantity,
                x.Price
            ))
            .OrderBy(x => x.Name)
            .ToList();
    }
}
