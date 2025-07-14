using Microsoft.AspNetCore.Diagnostics;
using Shared.ErrorModels;
using System.Net;

namespace ExaminationSystem.Web.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, $"Something Went Wrong {exception.Message}");
            var response = new ErrorDetails
            {
                Message = exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(response,cancellationToken);

            return true;
        }
    }
}
