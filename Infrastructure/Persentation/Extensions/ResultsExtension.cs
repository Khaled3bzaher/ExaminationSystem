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
                    return new OkObjectResult(apiResponse);

            return MapResultObject(apiResponse);
        }
        private static IActionResult MapResultObject<T>(APIResponse<T> apiResponse) {
            return apiResponse.StatusCode switch
            {
                StatusCodes.Status400BadRequest =>new BadRequestObjectResult(apiResponse),
                StatusCodes.Status401Unauthorized => new UnauthorizedObjectResult(apiResponse),
                StatusCodes.Status404NotFound => new NotFoundObjectResult(apiResponse),
                StatusCodes.Status409Conflict => new ConflictObjectResult(apiResponse),
                StatusCodes.Status403Forbidden => new ObjectResult(apiResponse) { StatusCode = StatusCodes.Status403Forbidden },
                _ => new ObjectResult(apiResponse) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
}
