using KayraExport.Microservices.BuildingBlocks.Shared.Application.Factories;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Abstraction.MediatR.Command
{
    /// <summary>
    /// Tüm projelerde kullanılan ve geriye veri dönmeyen command handler'ları soyutlayan sınıftır.
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

    /// <summary>
    /// Tüm projelerde kullanılan ve geriye veri dönen command handler'ları soyutlayan sınıftır.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class CommandHandlerBase<TCommand, TResponse> : IRequestHandler<TCommand, DataResponse<TResponse>> where TCommand : CommandBase<TResponse>
    {
        public abstract Task<DataResponse<TResponse>> Handle(TCommand request, CancellationToken cancellationToken);

        protected DataResponse<TResponse> OkResponse(TResponse data, string? message = null)
            => ResponseFactory.Ok(data, message);

        protected DataResponse<TResponse> CreatedResponse(TResponse data, string? message = null)
            => ResponseFactory.Created(data, message);

        protected DataResponse<TResponse> AcceptedResponse(string? message = null)
            => ResponseFactory.Accepted<TResponse>(message);

        protected DataResponse<TResponse> NoContentResponse(string? message = null)
            => ResponseFactory.NoContent<TResponse>(message);

        protected DataResponse<TResponse> BadRequestResponse(string? message = null)
            => ResponseFactory.BadRequest<TResponse>(message);

        protected DataResponse<TResponse> UnauthorizedResponse(string? message = null)
            => ResponseFactory.Unauthorized<TResponse>(message);

        protected DataResponse<TResponse> ForbiddenResponse(string? message = null)
            => ResponseFactory.Forbidden<TResponse>(message);

        protected DataResponse<TResponse> NotFoundResponse(string? message = null)
            => ResponseFactory.NotFound<TResponse>(message);

        protected DataResponse<TResponse> InternalServerErrorResponse(string? message = null)
            => ResponseFactory.InternalServerError<TResponse>(message);
    }
}
