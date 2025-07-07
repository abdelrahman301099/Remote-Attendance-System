using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Application.Services;

namespace NetBlaze.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISampleService, SampleService>();
        }
    }
}