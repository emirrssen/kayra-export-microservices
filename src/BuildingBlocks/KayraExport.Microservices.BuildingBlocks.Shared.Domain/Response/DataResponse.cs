using System.Net;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response
{
    /// <summary>
    /// Uygulama genelinde kullanılan ve veri taşıyan generic response modeldir.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class DataResponse<TData> : BaseResponse
    {
        public DataResponse() { }

        public DataResponse(HttpStatusCode statusCode, TData? data, string? message = null) : base(statusCode, message)
            => Data = data;

        public TData? Data { get; set; }
    }
}
