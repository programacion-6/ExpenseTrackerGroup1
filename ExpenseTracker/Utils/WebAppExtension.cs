using ExpenseTracker.Persistence.Database.Interface;

namespace ExpenseTracker.Utils;

public static class WebAppExtension
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInit>();

            dbInitializer.InitializeDatabase();

            return app;
        }
    }
}