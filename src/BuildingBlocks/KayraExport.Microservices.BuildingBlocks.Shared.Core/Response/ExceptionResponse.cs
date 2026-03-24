using Newtonsoft.Json;
using System.Net;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Core.Response
{
    /// <summary>
    /// Sistemsel hataları standart bir formatta geri dönülmesini sağlayan response modelidir.
    /// </summary>
    public class ExceptionResponse
    {
        [JsonProperty("code")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
