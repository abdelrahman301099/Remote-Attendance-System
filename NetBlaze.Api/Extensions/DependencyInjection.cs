using Microsoft.Extensions.Options;
using NetBlaze.Application.Extensions;
using NetBlaze.Infrastructure.Data.DatabaseContext;
using NetBlaze.Infrastructure.Extensions;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.Text.Json;

namespace NetBlaze.Api.Extensions
{
    public static class DependencyInjection
    {
        private static readonly string CORS_POLICY = nameof(CORS_POLICY);

        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.AddInfrastructureServices();

            builder.AddApplicationServices();

            builder.Services.AddLocalization();

            var supportedCultures = new[] { LanguageCode.ARABIC_CODE, LanguageCode.ENGLISH_CODE };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options
                    .SetDefaultCulture(LanguageCode.ARABIC_CODE)
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            });

            builder
                .Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = GlobalRequestValidationHandler.UseGlobalRequestValidationHandler())
                .AddDataAnnotationsLocalization();

            builder.Services.AddSwaggerWithBearerAndLanguage();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("HRorAdmin", policy => policy.RequireRole("HR", "Admin"));
                options.AddPolicy("CanManageVacations", policy => policy.RequireRole("HR", "Admin"));
                options.AddPolicy("CanManageCompanyPolicies", policy => policy.RequireRole("HR", "Admin"));
                options.AddPolicy("CanManageUsers", policy => policy.RequireRole("Admin"));
                options.AddPolicy("CanViewReports", policy => policy.RequireRole("Admin", "HR"));
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void ConsumeServices(this WebApplication app)
        {
            app.InitializeDatabaseAsync().ConfigureAwait(false);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(CORS_POLICY);

            app.UseGlobalExceptionHandler(app.Environment);

            var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;

            app.UseRequestLocalization(localizationOptions);

            app.UseResponseCompression();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
