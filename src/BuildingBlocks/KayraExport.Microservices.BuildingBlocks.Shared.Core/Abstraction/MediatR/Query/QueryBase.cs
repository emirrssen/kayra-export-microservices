using KayraExport.Microservices.BuildingBlocks.Shared.Core.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Abstraction.MediatR.Query
{
    /// <summary>
    /// Tüm projelerde kullanılacak query'leri soyutlayan sınıftır.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class QueryBase<TResponse> : IRequest<DataResponse<TResponse>>, IMediatrAbstractionCore { }
}
