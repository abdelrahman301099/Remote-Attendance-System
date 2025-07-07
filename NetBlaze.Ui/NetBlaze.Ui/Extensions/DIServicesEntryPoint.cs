using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.Ui.Client.Extensions;
using NetBlaze.Ui.Client.InternalHelperTypes.General;
using NetBlaze.Ui.SharedRazor;

namespace NetBlaze.Ui.Extensions
{
    public static class DIServicesEntryPoint
    {
        public static void RegisterServerServices(this WebApplicationBuilder builder)
        {
            builder
                .Services
                .AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddControllers();

            var urlConfiguration = builder.Configuration.GetSection(nameof(UrlConfiguration)).Get<UrlConfiguration>()!;

            builder.Services.RegisterClientServices(urlConfiguration);
        }

        public static void ConsumeServerServices(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            var supportedCultures = new[] { LanguageCode.ARABIC_CODE, LanguageCode.ENGLISH_CODE };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAntiforgery();

            app.MapControllers();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(NetBlaze.Ui.Client._Imports).Assembly);
        }
    }
}
