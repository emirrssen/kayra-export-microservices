using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Exceptions;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Mİddlewares
{
    public class GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidatorException exception)
            {
                // TODO -> Bu kısmı logla.
                await HandleValidatorExceptionAsync(context, exception);
            }
            catch (Exception exception)
            {
                // TODO -> Bu kısmı logla.
                await HandleExceptionAsync(context, exception);
            }
        }

        /// <summary>
        /// Sistem genelinde kontrol altına alınmayan hatalar meydana geldiğinde 500 status code'lu standart bir response dönülmesini sağlar.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            ExceptionResponse exceptionResponse = new() { Message = "Unexpected error occurred!", StatusCode = statusCode };
            var resultException = JsonConvert.SerializeObject(exceptionResponse);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(resultException);
        }

        /// <summary>
        /// Gelen isteklarda validasyon kurallarına aykırı request gelmesi neticesinde standart response mesaj dönülmesini sağlar.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private Task HandleValidatorExceptionAsync(HttpContext context, ValidatorException exception)
        {
            var statusCode = HttpStatusCode.BadRequest;
            ExceptionResponse exceptionResponse = new() { Message = exception.Message, StatusCode = statusCode };
            var resultException = JsonConvert.SerializeObject(exceptionResponse);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(resultException);
        }
    }
}
