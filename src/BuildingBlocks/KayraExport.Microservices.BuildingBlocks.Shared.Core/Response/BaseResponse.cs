using System.Net;
using System.Text.Json.Serialization;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Response
{
    /// <summary>
    /// Uygulama genelinde kullanılan temel response modelidir. 
    /// </summary>
    public class BaseResponse
    {
        public BaseResponse() { }

        public BaseResponse(HttpStatusCode statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public string? Message { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonIgnore]
        public bool IsOk { get => (long)StatusCode >= 200 && (long)StatusCode < 300; }
    }
}
