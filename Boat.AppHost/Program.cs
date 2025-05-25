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
    .WaitFor(sqldb)
    .WithExternalHttpEndpoints();

builder.AddNpmApp("Angular", "../Boat.ClientApp/Client")
    .WithReference(apiService)
    .WaitFor(apiService)    
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile()
    .WithEnvironment("API_BASE_URL", apiService.GetEndpoint("http")); 

//builder.AddProject<Projects.Boat_Gateway>("boat-gateway")
//   .WithReference(apiService)
//   .WaitFor(apiService)
//   .WithExternalHttpEndpoints();

builder.Build().Run();
