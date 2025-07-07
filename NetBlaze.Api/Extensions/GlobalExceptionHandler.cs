using Microsoft.AspNetCore.Diagnostics;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;
using System.Text.Json;

namespace NetBlaze.Api.Extensions
{
    public static class GlobalExceptionHandler
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = MiscConstants.ApplicationJsonContentType;

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature?.Error == null)
                    {
                        return;
                    }

                    var exception = exceptionHandlerFeature.Error;

                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                    logger.LogError(exception, "Unhandled exception occurred");

                    var (statusCode, errorDetails) = MapExceptionToStatusCode(exception, env.IsDevelopment());

                    context.Response.StatusCode = (int)statusCode;

                    var response = new ApiResponse<object>(
                        null,
                        false,
                        env.IsDevelopment() ? exception.Message : Messages.ErrorOccurredInServer,
                        statusCode,
                        [errorDetails!]
                    );

                    var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = env.IsDevelopment()
                    });

                    await context.Response.WriteAsync(json);
                });
            });

            return app;
        }

        private static (HttpStatusCode StatusCode, string? ErrorDetails) MapExceptionToStatusCode(Exception exception, bool isDevelopment)
        {
            return exception switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, isDevelopment ? exception.StackTrace : null),
                KeyNotFoundException => (HttpStatusCode.NotFound, isDevelopment ? exception.StackTrace : null),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, isDevelopment ? exception.StackTrace : null),
                InvalidOperationException => (HttpStatusCode.Conflict, isDevelopment ? exception.StackTrace : null),
                _ => (HttpStatusCode.InternalServerError, isDevelopment ? exception.StackTrace : null)
            };
        }
    }
}