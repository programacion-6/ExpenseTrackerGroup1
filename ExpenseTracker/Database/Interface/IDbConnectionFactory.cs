using System.Data;

namespace ExpenseTracker.Persistence.Database.Interface;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}