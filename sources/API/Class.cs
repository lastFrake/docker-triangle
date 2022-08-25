using Microsoft.EntityFrameworkCore;

namespace API;

internal static class T
{
    public static async Task MigrateDatabaseAsync<T>(this IHost webHost) where T : DbContext
    {
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                if ((await db.Database.GetPendingMigrationsAsync()).Any())
                {
                    await db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}