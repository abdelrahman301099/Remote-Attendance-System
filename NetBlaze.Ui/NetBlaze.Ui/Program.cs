using NetBlaze.Ui.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServerServices();

var app = builder.Build();

app.ConsumeServerServices();

await app.RunAsync();
