using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using System.Net;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Factories
{
    /// <summary>
    /// Tüm mikroservislerde kullanılacak olan temel response modellerinin pratik bir şekilde oluşturulmasını amaçlar.
    /// </summary>
    public static class ResponseFactory
    {
        #region BaseResponse

        public static BaseResponse Ok(string? message = null) => new(HttpStatusCode.OK, message);
        public static BaseResponse Created(string? message = null) => new(HttpStatusCode.Created, message);
        public static BaseResponse Accepted(string? message = null) => new(HttpStatusCode.Accepted, message);
        public static BaseResponse NoContent(string? message = null) => new(HttpStatusCode.NoContent, message);
        public static BaseResponse BadRequest(string? message = null) => new(HttpStatusCode.BadRequest, message);
        public static BaseResponse Unauthorized(string? message = null) => new(HttpStatusCode.Unauthorized, message);
        public static BaseResponse Forbidden(string? message = null) => new(HttpStatusCode.Forbidden, message);
        public static BaseResponse NotFound(string? message = null) => new(HttpStatusCode.NotFound, message);
        public static BaseResponse InternalServerError(string? message = null) => new(HttpStatusCode.InternalServerError, message);

        #endregion

        #region Data Response

        public static DataResponse<TData> Ok<TData>(TData data, string? message = null) => new(HttpStatusCode.OK, data, message);
        public static DataResponse<TData> Created<TData>(TData data, string? message = null) => new(HttpStatusCode.Created, data, message);
        public static DataResponse<TData> Accepted<TData>(string? message = null) => new(HttpStatusCode.Accepted, default, message);
        public static DataResponse<TData> NoContent<TData>(string? message = null) => new(HttpStatusCode.NoContent, default, message);
        public static DataResponse<TData> BadRequest<TData>(string? message = null) => new(HttpStatusCode.BadRequest, default, message);
        public static DataResponse<TData> Unauthorized<TData>(string? message = null) => new(HttpStatusCode.Unauthorized, default, null);
        public static DataResponse<TData> Forbidden<TData>(string? message = null) => new(HttpStatusCode.Forbidden, default, message);
        public static DataResponse<TData> NotFound<TData>(string? message = null) => new(HttpStatusCode.NotFound, default, message);
        public static DataResponse<TData> InternalServerError<TData>(string? message = null) 
            => new(HttpStatusCode.InternalServerError, default, message);

        #endregion
    }
}
