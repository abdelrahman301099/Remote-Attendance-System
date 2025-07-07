using System.Net;
using System.Text.Json.Serialization;

namespace NetBlaze.SharedKernel.HelperUtilities.General
{
    public class ApiResponse<T>
    {
        public T? Data { get; private init; }
        public bool Success { get; private init; } = false;
        public string Message { get; private init; } = string.Empty;
        public HttpStatusCode StatusCode { get; private init; } = HttpStatusCode.InternalServerError;
        public string[]? Error { get; private init; }

        public ApiResponse() { }

        [JsonConstructor]
        public ApiResponse(
            T? data,
            bool success,
            string message,
            HttpStatusCode statusCode,
            string[]? error
        )
        {
            Data = data;
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Error = error;
        }

        public static ApiResponse<T> ReturnSuccessResponse(T? data, string message = "Request succeeded", HttpStatusCode statusCode = HttpStatusCode.OK) =>
            new(data, true, message, statusCode, null);

        public static ApiResponse<T> ReturnFailureResponse(string message = "Request failed", HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string[]? error = null) =>
            new(default, false, message, statusCode, error);
    }
}
