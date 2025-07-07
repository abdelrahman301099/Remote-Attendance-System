using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetBlaze.Ui.Client.Extensions;
using NetBlaze.Ui.Client.InternalHelperTypes.General;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var urlConfiguration = builder.Configuration.GetSection(nameof(UrlConfiguration)).Get<UrlConfiguration>()!;

builder.Services.RegisterClientServices(urlConfiguration);

var host = builder.Build();

await host.ConsumeClientServicesAsync();

await host.RunAsync();
