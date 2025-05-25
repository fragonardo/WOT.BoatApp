using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var sqldb = builder.AddSqlServer("sql")
    .AddDatabase("boatdb")
    //.WithLifetime(ContainerLifetime.Persistent)
    ;

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.BoatApp_ApiService>("boat-service")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(sqldb)
    .WaitFor(sqldb)
    ;

var bff = builder.AddProject<Projects.Boat_Bff>("boat-bff")
    .WithReference(apiService)    
    .WithExternalHttpEndpoints()
    ;


var client = builder.AddNpmApp("Angular", "../Boat.ClientApp/Client")
    .WithReference(bff)
    .WaitFor(bff)    
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile()
    .WithEnvironment("API_BASE_URL", bff.GetEndpoint("http"));





builder.Build().Run();
