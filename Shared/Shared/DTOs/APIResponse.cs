using System.Net;

namespace Shared.DTOs
{
    public class APIResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static APIResponse<T> SuccessResponse(T? data, string message = "")
        {
            return new APIResponse<T>
            { Success = true, StatusCode = (int)HttpStatusCode.OK, Data = data, Message = message };
        }
        public static APIResponse<T> FailureResponse(int statusCode, string message)
        {
            return new APIResponse<T>
            { Success = false, StatusCode = statusCode, Message = message };
        }

    }
}
