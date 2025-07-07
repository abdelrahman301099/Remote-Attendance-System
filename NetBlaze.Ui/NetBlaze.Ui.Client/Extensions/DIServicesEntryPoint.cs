using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.Ui.Client.InternalHelperTypes.General;
using NetBlaze.Ui.Client.Services;
using NetBlaze.Ui.Client.Services.CommonServices;
using System.Globalization;

namespace NetBlaze.Ui.Client.Extensions
{
    public static class DIServicesEntryPoint
    {
        public static void RegisterClientServices(this IServiceCollection services, UrlConfiguration urlConfiguration)
        {
            services.AddTransient<HttpRequestHandler>();

            services
                .AddHttpClient<ExternalHttpClientWrapper>(client => client.BaseAddress = new Uri(urlConfiguration.ApiBaseUrl))
                .AddHttpMessageHandler<HttpRequestHandler>();

            services
                .AddHttpClient<InternalHttpClientWrapper>(client => client.BaseAddress = new Uri(urlConfiguration.UiBaseUrl));

            services.AddHttpContextAccessor();

            services.AddLocalization();

            services.AddMudServices();

            services.AddScoped<CentralizedSnackbarProvider>();

            services.AddBlazoredLocalStorage();

            services.AddScoped<CookieService>();


            // ADD BLAZOR SERVICES HERE:

            services.AddScoped<BlazSampleService>();
        }

        public static async Task ConsumeClientServicesAsync(this WebAssemblyHost app)
        {
            var cookieService = app.Services.GetRequiredService<CookieService>();

            var currentCulture = await cookieService.GetCookieAsync(MiscConstants.currentCultureCode);

            string languageCode = LanguageCode.ENGLISH_CODE;

            if (!string.IsNullOrWhiteSpace(currentCulture))
            {
                languageCode = currentCulture;
            }

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(languageCode);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(languageCode);

            await Task.CompletedTask;
        }
    }
}