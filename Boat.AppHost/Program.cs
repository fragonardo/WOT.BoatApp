var builder = DistributedApplication.CreateBuilder(args);

var sqldb = builder.AddSqlServer("sql")
    .AddDatabase("boatdb")
    //.WithLifetime(ContainerLifetime.Persistent)
    ;

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.BoatApp_ApiService>("apiservice")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(sqldb)
    .WaitFor(sqldb);

builder.AddProject<Projects.Boat_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.Boat_Gateway>("boat-gateway")
   .WithReference(apiService)
   .WaitFor(apiService)
   .WithExternalHttpEndpoints();

builder.Build().Run();
