using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net;
using System.Text.Json;

namespace NetBlaze.Api.Extensions
{
    public static class GlobalRequestValidationHandler
    {
        public static Func<ActionContext, IActionResult> UseGlobalRequestValidationHandler() => context =>
        {
            var errorMessages = context.ModelState
                .SelectMany(ms => ms.Value?.Errors ?? Enumerable.Empty<ModelError>())
                .Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? "Invalid input" : e.ErrorMessage)
                .ToList();

            var message = errorMessages.Count != 0 ? string.Join("; ", errorMessages) : "Validation failed";

            var response = new ApiResponse<object>(
                null,
                false,
                message,
                HttpStatusCode.BadRequest,
                ["Request validation failed"]
            );

            return new JsonResult(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = context.HttpContext.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment()
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        };
    }
}