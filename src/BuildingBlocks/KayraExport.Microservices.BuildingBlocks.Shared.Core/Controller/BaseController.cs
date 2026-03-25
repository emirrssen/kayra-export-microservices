using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Controller
{
    [ApiController]
    public class BaseController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Tüm projede kullanılacak temel response model dönen endpoint'ler için ortak metot.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [NonAction]
        protected async Task<IActionResult> ExecuteAsync(IRequest<BaseResponse> request)
        {
            var result = await mediator.Send(request);
            return MapWithStatusCodeResponse(result.StatusCode, result, result.Message ?? "");
        }

        /// <summary>
        /// Tüm projelerde kullanılacak veri taşıyan response model dönen endpoint'ler için ortak metot.
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        [NonAction]
        protected async Task<IActionResult> ExecuteAsync<TResponse>(IRequest<DataResponse<TResponse>> request)
        {
            var result = await mediator.Send(request);
            return MapWithStatusCodeResponse(result.StatusCode, result, result.Message ?? "");
        }

        /// <summary>
        /// Dönen response'u uygun HTTP cevabına dönüştüren yardımcı metot.
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="response"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        private IActionResult MapWithStatusCodeResponse(HttpStatusCode statusCode, object response, string message = "")
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK: return Ok(response);
                case HttpStatusCode.Created: return StatusCode(StatusCodes.Status201Created, response);
                case HttpStatusCode.Accepted: return Accepted(message);
                case HttpStatusCode.NoContent: return NoContent();
                case HttpStatusCode.BadRequest: return BadRequest(response);
                case HttpStatusCode.Unauthorized: return Unauthorized();
                case HttpStatusCode.NotFound: return NotFound(message);
                case HttpStatusCode.Forbidden: return Forbid();
                case HttpStatusCode.InternalServerError: return StatusCode(StatusCodes.Status500InternalServerError, message);
                default: return Ok();
            }
        }
    }
}
