using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetBlaze.Application.Services;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Identity;
using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Application.Extensions
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddScoped<ISampleService, SampleService>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IResetPasswordService, ResetPasswordService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICompanyPolicyService, CompanyPolicyService>();
            builder.Services.AddScoped<IVacationsService, VacationsService>();


        }
    }
}
