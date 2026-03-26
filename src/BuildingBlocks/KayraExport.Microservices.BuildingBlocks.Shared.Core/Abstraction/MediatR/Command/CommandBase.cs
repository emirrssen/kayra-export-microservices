using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command
{
    /// <summary>
    /// Tüm projelerde kullanılan ve geriye veri dönmeyen command'leri soyutlyan sınıftır.
    /// </summary>
    public abstract class CommandBase : IRequest<BaseResponse>, IMediatrAbstractionCore { }

    /// <summary>
    /// Tüm projelerde kullanılan ve geriye veri dönen command'leri soyutlayan sınıftır.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class CommandBase<TResponse> : IRequest<DataResponse<TResponse>>, IMediatrAbstractionCore { }
}
