using KayraExport.Microservices.BuildingBlocks.Shared.Core.Factories;
using KayraExport.Microservices.BuildingBlocks.Shared.Core.Response;
using MediatR;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Abstraction.MediatR.Query
{
    /// <summary>
    /// Tüm projelerde kullanılacak query handler'ları soyutlayan sınıftır.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class QueryHandlerBase<TQuery, TResponse> : IRequestHandler<TQuery, DataResponse<TResponse>>
        where TQuery : QueryBase<TResponse>
    {
        public abstract Task<DataResponse<TResponse>> Handle(TQuery request, CancellationToken cancellationToken);

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
