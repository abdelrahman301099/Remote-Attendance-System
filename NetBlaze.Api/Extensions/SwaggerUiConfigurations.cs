using Microsoft.OpenApi.Models;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace NetBlaze.Api.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerWithBearerAndLanguage(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CES APIs",
                    Version = "v1",
                    Description = "API documentation with support for ar-EG and en-US languages"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token. The word 'Bearer' will be added automatically."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                c.OperationFilter<LanguageHeaderOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithLanguageSelection(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "CES APIs v1");

                c.RoutePrefix = "api-docs";

                c.DocExpansion(DocExpansion.None);

                c.HeadContent = @"
                    <style>
                        #language-selector {
                            margin: 10px 0;
                            padding: 5px;
                            border-radius: 4px;
                            border: 1px solid #ccc;
                        }
                        #language-selector option[value='ar-EG'] {
                            direction: rtl;
                        }
                    </style>";

                c.DocumentTitle = "CES APIs Documentation";

                c.UseRequestInterceptor(@"
                    function(request) { 
                        const language = localStorage.getItem('selected-language') || 'en-US';
                        request.headers['Accept-Language'] = language;
                        return request;
                    }
                ");
            });

            return app;
        }
    }

    public class LanguageHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Enum =
                    {
                        new Microsoft.OpenApi.Any.OpenApiString(LanguageCode.ENGLISH_CODE),
                        new Microsoft.OpenApi.Any.OpenApiString(LanguageCode.ARABIC_CODE)
                    },
                    Default = new Microsoft.OpenApi.Any.OpenApiString(LanguageCode.ARABIC_CODE)
                },
                Description = "Select language (English or Arabic)"
            });
        }
    }
}