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

    public async Task<Budget?> UpdateEntity(Guid entityId, IDto<Budget> entityDto)
    {
        var sql = @"UPDATE Budget 
                        SET BudgetAmount = @BudgetAmount 
                        WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();

        await connection.ExecuteAsync(sql, new { Id = entityId, BudgetAmount = entityDto.GetEntity(null).BudgetAmount });
        return await ReadEntity(entityId);
    }

    public async Task<Budget> GetMonthlyBudget(Guid userId, DateTime month)
    {
        var sql = @"SELECT * FROM Budget 
                        WHERE UserId = @UserId AND Month = @Month";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var budget = await connection.QueryFirstOrDefaultAsync<Budget>(sql, new { UserId = userId, Month = month.ToString("yyyy-MM") });
        return budget ?? throw new KeyNotFoundException("Budget not found for the given month.");
    }

    public async Task<Budget> GetRemainingBudget(Guid userId)
    {
        var sql = @"SELECT * FROM Budget 
                        WHERE UserId = @UserId 
                        ORDER BY Month DESC LIMIT 1";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryFirstOrDefaultAsync<Budget>(sql, new { UserId = userId });
    }
}