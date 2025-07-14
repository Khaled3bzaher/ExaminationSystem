using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace ExaminationSystem.Web.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                await HandleExceptionsAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionsAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                Message = ex.Message
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetValidationErrors(badRequestException,response),
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = response.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(response);
        }

        private static int GetValidationErrors(BadRequestException badRequestException, ErrorDetails response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                httpContext.Response.ContentType = "application/json";
                var response = new ErrorDetails
                {
                    Message = $"End Point {httpContext.Request.Path} Not Found.!",
                    StatusCode = httpContext.Response.StatusCode
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
