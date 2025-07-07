using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Text;

namespace NetBlaze.Infrastructure.Extensions
{
    public static class BearerAuthenticationService
    {
        public static void AddBearerAuthenticationService(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

                var key = Encoding.ASCII.GetBytes(jwtSettings!.Key);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }
    }
}
