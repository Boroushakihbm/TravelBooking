using Microsoft.EntityFrameworkCore;
using TravelBooking.Infrastructure.mssql.Persistence;

namespace TravelBooking.GatewayApi.Configuration;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using (var scope = webApp.Services.CreateScope())
        {

            using (var appContext = scope.ServiceProvider.GetRequiredService<TravelBookingDbContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        return webApp;
    }
}
