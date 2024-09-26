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
        var sql = @"INSERT INTO Expenses (Id, UserId, Amount, Description, Category, Date, CreatedAt) 
                       VALUES (@Id, @UserId, @Amount, @Description, @Category, @Date, @CreatedAt)";
        using var connection = await _dbConnection.CreateConnectionAsync();
        await connection.ExecuteAsync(sql, entityModel);
        return entityModel;
    }

    public async Task<Expense?> DeleteEntity(Guid entityId)
    {
        var sql = "DELETE FROM Expenses WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(sql, new { Id = entityId });
        return result > 0 ? new Expense() : null; 
    }

    public async Task<List<Expense>> GetAllEntities()
    {
        var sql = "SELECT * FROM Expenses";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var expenses = await connection.QueryAsync<Expense>(sql);
        return expenses.ToList();
    }

    public async Task<Expense?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Expenses WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        var expense = await connection.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = entityId });
        return expense;
    }

    public async Task<Expense?> UpdateEntity(Guid entityId, IDto<Expense> entityDto)
    {
        var sqlSelect = "SELECT * FROM Expenses WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();

    
        var existingExpense = await connection.QuerySingleOrDefaultAsync<Expense>(sqlSelect, new { Id = entityId });
        if (existingExpense == null)
        {
            return null; 
        }

    
        var updatedExpense = entityDto.GetDto();
        existingExpense.UpdateDetails(updatedExpense.Amount, updatedExpense.Category, updatedExpense.Description);
        existingExpense.Date = updatedExpense.Date;

    
        var sqlUpdate = @"UPDATE Expenses 
                        SET Amount = @Amount, Description = @Description, Category = @Category, Date = @Date 
                        WHERE Id = @Id";
    
        var result = await connection.ExecuteAsync(sqlUpdate, existingExpense);
        return result > 0 ? existingExpense : null;
    }
}