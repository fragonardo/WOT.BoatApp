var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddReverseProxy();

// Add services to the container.

var app = builder.Build();

app.MapReverseProxy();

app.Run();

