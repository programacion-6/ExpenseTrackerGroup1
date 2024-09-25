using DbUp;
using ExpenseTracker.Persistence;
using ExpenseTracker.Persistence.Database.Interface;
using Microsoft.Extensions.Options;

public class DbInit : IDbInit
{
    private readonly DatabaseOptions _options;
    
    public DbInit(IOptions<DatabaseOptions> options)
    {
        _options = options.Value;
    }
    
    public void InitializeDatabase()
    {
        EnsureDatabase.For.PostgresqlDatabase(_options.DefaultConnection);

        // Obtener la ruta desde la configuración
        var scriptsPath = Path.Combine(Directory.GetCurrentDirectory(), _options.ScriptsPath);
        
        if (!Directory.Exists(scriptsPath))
        {
            Console.WriteLine($"La carpeta de scripts no existe: {scriptsPath}");
            return;
        }

        var dpUp = DeployChanges.To
            .PostgresqlDatabase(_options.DefaultConnection)
            .WithScriptsFromFileSystem(scriptsPath)  // Cargar scripts desde la carpeta filesystem
            .LogToConsole() 
            .LogScriptOutput() 
            .WithTransaction()
            .Build();

        var result = dpUp.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Invalid Migrations");
        }
    }
}