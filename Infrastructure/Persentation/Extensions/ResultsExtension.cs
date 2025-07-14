using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace Persentation.Extensions
{
    public static class ResultsExtension
    {
        public static IActionResult ToActionResult<T>(this APIResponse<T> apiResponse)
        {
            if (apiResponse.Success)
                if(apiResponse.Data is not null)
                    return new OkObjectResult(apiResponse.Data);
                else
                    return new OkObjectResult(apiResponse.Message);

            return MapResultObject(apiResponse);
        }
        private static IActionResult MapResultObject<T>(APIResponse<T> apiResponse) {
            return apiResponse.StatusCode switch
            {
                StatusCodes.Status400BadRequest =>new BadRequestObjectResult(apiResponse.Message),
                StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(apiResponse.Message),
                StatusCodes.Status404NotFound => new NotFoundObjectResult(apiResponse.Message),
                StatusCodes.Status409Conflict => new ConflictObjectResult(apiResponse.Message),
                _ => new ObjectResult(apiResponse.Message) { StatusCode = 500 }
            };
        }
    }
}
