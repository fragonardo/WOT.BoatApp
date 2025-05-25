using BoatApp.ApiService.EndPoints;
using BoatApp.ApiService.Extension;
using BoatApp.Application.Queries.Handlers;
using BoatApp.Domain.Repository;
using BoatApp.Infrastructure.Persistence;
using BoatApp.Infrastructure.Persistence.Repository;
using BoatApp.Infrastructure.Services.Identity;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddMediatR(cfg =>
{   
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
    cfg.RegisterServicesFromAssemblyContaining(typeof(GetAllBoatQueryHandler));
    //cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll",policy =>
                      {
                          policy                          
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          ;
                      });
});


builder.AddSqlServerDbContext<BoatDbContext>("boatdb");

var app = builder.Build();    

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MigrateDatabase();
}

app.MapDefaultEndpoints();

app.MapBoatsApi();

if(!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("allowAll");

app.UseCors();

app.Run();

