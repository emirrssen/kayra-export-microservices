using KayraExport.Microservices.BuildingBlocks.Shared.Application.Factories;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command
{
    /// <summary>
    /// Tüm projelerde kullanılan command handler'ları soyutlayan sınıftır.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public abstract class CommandHandlerBase<TCommand> : IRequestHandler<TCommand, BaseResponse> where TCommand : CommandBase
    {
        public abstract Task<BaseResponse> Handle(TCommand request, CancellationToken cancellationToken);

        protected BaseResponse OkResponse(string? message = null) => ResponseFactory.Ok(message);
        protected BaseResponse CreatedResponse(string? message = null) => ResponseFactory.Created(message);
        protected BaseResponse AcceptedResponse(string? message = null) => ResponseFactory.Accepted(message);
        protected BaseResponse NoContentResponse(string? message = null) => ResponseFactory.NoContent(message);
        protected BaseResponse BadRequestResponse(string? message = null) => ResponseFactory.BadRequest(message);
        protected BaseResponse UnauthorizedResponse(string? message = null) => ResponseFactory.Unauthorized(message);
        protected BaseResponse ForbiddenResponse(string? message = null) => ResponseFactory.Forbidden(message);
        protected BaseResponse NotFoundResponse(string? message = null) => ResponseFactory.NotFound(message);
        protected BaseResponse InternalServerErrorResponse(string? message = null) => ResponseFactory.InternalServerError(message);
    }
}
