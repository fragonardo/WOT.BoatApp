using BoatApp.Infrastructure.Persistence;

namespace BoatApp.ApiService.Extension;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using (var dbContext = scope.ServiceProvider.GetRequiredService<BoatDbContext>())
            {
                try
                {
                    dbContext.Database.EnsureDeleted();
                    dbContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
        }
        return app;
    }
}
