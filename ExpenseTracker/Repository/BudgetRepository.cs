using System.Data;
using Dapper;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Persistence.Database.Interface;

namespace ExpenseTracker.Repository;

public class BudgetRepository : IBudgetRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public BudgetRepository(IDbConnectionFactory dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Budget?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Budget WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<Budget>(sql, new { Id = entityId });
    }

    public async Task<Budget> CreateEntity(Budget entityModel)
    {
        var sql = @"INSERT INTO Budget (Id, UserId, BudgetAmount, Month) 
                        VALUES (@Id, @UserId, @BudgetAmount, @Month)";
        using var connection = await _dbConnection.CreateConnectionAsync();
        await connection.ExecuteAsync(sql, entityModel);
        return entityModel;
    }

    public async Task<bool> UpdateEntity(Guid entityId, Budget entity)
    {
        var sql = @"UPDATE Budget 
                        SET BudgetAmount = @BudgetAmount 
                        WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, entity);
        return affectedRows > 0;
    }

    public async Task<Budget?> GetMonthlyBudget(Guid userId, DateTime month)
    {
        var sql = @"SELECT * FROM Budget 
                WHERE UserId = @UserId AND Month = @Month";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
        return await connection.QueryFirstOrDefaultAsync<Budget>(sql, new { UserId = userId, Month = firstDayOfMonth });
    }
    
    public async Task<Budget?> GetCurrentMonthBudget(Guid userId)
    {
        var sql = @"SELECT * FROM Budget 
                WHERE UserId = @UserId AND Month = @Month";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        return await connection.QueryFirstOrDefaultAsync<Budget>(sql, new { UserId = userId, Month = currentMonth });
    }

}