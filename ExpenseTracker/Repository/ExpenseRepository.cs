using System.Data;
using Dapper;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Dtos.ExpenseDtos;
using ExpenseTracker.Persistence.Database.Interface;

namespace ExpenseTracker.Repository;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public ExpenseRepository(IDbConnectionFactory dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Expense> CreateEntity(Expense entityModel)
    {
        var sql = @"INSERT INTO Expense (Id, UserId, Amount, Description, Category, Date, CreatedAt) 
                       VALUES (@Id, @UserId, @Amount, @Description, @Category, @Date, @CreatedAt)";
        using var connection = await _dbConnection.CreateConnectionAsync();
        await connection.ExecuteAsync(sql, entityModel);
        return entityModel;
    }

    public async Task<Expense?> DeleteEntity(Guid entityId)
    {
        var sql = "DELETE FROM Expense WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = entityId });
        return null; 
    }

    public async Task<List<Expense>> GetAllEntities()
    {
        var sql = "SELECT * FROM Expense";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return (await connection.QueryAsync<Expense>(sql)).AsList();
    }

    public async Task<Expense?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Expense WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var expense = await connection.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = entityId });
        return expense;
    }

    public async Task<bool> UpdateEntity(Guid entityId, Expense entity)
    {
        var sql = @"UPDATE Expense
                    SET Amount = @Amount,
                        Description = @Description,
                        Category = @Category,
                        Date = @Date
                    WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var affectedRows = await connection.ExecuteAsync(sql, entity);
        return affectedRows > 0;
    }

    
}