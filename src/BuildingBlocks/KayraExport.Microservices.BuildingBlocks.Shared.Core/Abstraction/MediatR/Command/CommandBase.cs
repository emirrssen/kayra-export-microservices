using KayraExport.Microservices.BuildingBlocks.Shared.Core.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Abstraction.MediatR.Command
{
    /// <summary>
    /// Tüm projelerde kullanılan command'leri soyutlyan sınıftır.
    /// </summary>
    public abstract class CommandBase : IRequest<BaseResponse>, IMediatrAbstractionCore { }
}
