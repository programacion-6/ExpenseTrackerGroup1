using System.Data;
using ExpenseTracker.Persistence.Database.Interface;
using Microsoft.Extensions.Options;
using Npgsql;

namespace ExpenseTracker.Persistence.Database.Infrastructure;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly DbOptions _options;

    public DbConnectionFactory(IOptions<DbOptions> options)
    {
        _options = options.Value;
    }
    
    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_options.DefaultConnection);
        await connection.OpenAsync();
        return connection;
    }
}