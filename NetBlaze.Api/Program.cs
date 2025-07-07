using NetBlaze.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

app.ConsumeServices();

await app.RunAsync();
