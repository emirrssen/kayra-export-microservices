using KayraExport.Microservices.BuildingBlocks.Shared.Application.Events;
using KayraExport.Microservices.BuildingBlocks.Shared.Application.Helpers;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Enums;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Exceptions;
using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Response;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Rebus.Bus;
using System.Net;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Middlewares
{
    public class GlobalErrorHandlerMiddleware(RequestDelegate next, string serviceName)
    {
        public async Task Invoke(HttpContext context, IBus bus)
        {
            try
            {
                await next(context);
            }
            catch (ValidatorException exception)
            {
                await TrySendLogEvent(bus, exception.Message, LogLevelEnum.Error, "Validation failed", serviceName);
                await HandleValidatorExceptionAsync(context, exception);
            }
            catch (Exception exception)
            {
                await TrySendLogEvent(bus, exception.Message, LogLevelEnum.Error, "Unexpected error occured", serviceName);
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task TrySendLogEvent(IBus bus, string details, LogLevelEnum level, string message, string serviceName)
        {
            try 
            {
                await bus.Send(new LogMessageEvent
                {
                    CreatedAt = DateTimeHelper.GetNowByTurkiyeTimeZone(),
                    ExceptionDetails = details,
                    LogLevel = level,
                    Message = message,
                    ServiceName = serviceName
                });
            } 
            catch 
            {
                // Message broker kapalıysa hatayı yut (Silently Fail), 
                // böylece sistem asıl dönmesi gereken standart API Response'u dönmeye devam edebilir.
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
