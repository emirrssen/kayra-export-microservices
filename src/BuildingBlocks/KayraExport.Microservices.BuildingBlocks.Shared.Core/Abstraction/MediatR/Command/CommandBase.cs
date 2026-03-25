using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command
{
    /// <summary>
    /// Tüm projelerde kullanılan command'leri soyutlyan sınıftır.
    /// </summary>
    public abstract class CommandBase : IRequest<BaseResponse>, IMediatrAbstractionCore { }
}
