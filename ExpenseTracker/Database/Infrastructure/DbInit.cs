using System.Reflection;
using DbUp;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.Database.Interface;
using Microsoft.Extensions.Options;

public class DbInit : IDbInit
{
    private readonly DbOptions _options;
    public DbInit(IOptions<DbOptions> options)
    {
        _options = options.Value;
    }
    public void InitializeDatabase()
    {
        EnsureDatabase.For.PostgresqlDatabase(_options.DefaultConnection);

        var dpUp = DeployChanges.To
            .PostgresqlDatabase(_options.DefaultConnection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransaction()
            .LogToConsole()
            .Build();

        var result = dpUp.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Invalid Migrations");
        }
    }
}