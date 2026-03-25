using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Query
{
    /// <summary>
    /// Tüm projelerde kullanılacak query'leri soyutlayan sınıftır.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class QueryBase<TResponse> : IRequest<DataResponse<TResponse>>, IMediatrAbstractionCore { }
}
