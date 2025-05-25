using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var boatServiceUrl = builder.Configuration["services:boat-service:http:0"];
if (string.IsNullOrEmpty(boatServiceUrl))
    throw new InvalidOperationException("Boat service url is not configured.");


builder.Services.AddReverseProxy()
    .LoadFromMemory(new[]
        {
            new RouteConfig
            {
                RouteId = "boats",
                ClusterId = "boatService",
                Match = new RouteMatch
                {
                    Path = "/api/boats/{**catch-all}"
                }
            }
        },
        new[]
        {
            new ClusterConfig
            {
                ClusterId = "boatService",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    {
                        "boatService/destination1", new DestinationConfig
                        {
                            Address = boatServiceUrl.TrimEnd('/') + "/"
                        }
                    }
                }
            }
        }
    );

var allowedOrigin = "http://localhost:4200"; // ou l’URL de ton Angular

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigin)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();

app.Run();