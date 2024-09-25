using System.Data;
using Dapper;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Dtos.ExpenseDtos;

namespace ExpenseTracker.Repository;

public class ExpenseRepository : IExpenseRepository
{
    private readonly IDbConnection _dbConnection;

    public ExpenseRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Expense> CreateEntity(Expense entityModel)
    {
        var sql = @"INSERT INTO Expenses (Id, UserId, Amount, Description, Category, Date, CreatedAt) 
                       VALUES (@Id, @UserId, @Amount, @Description, @Category, @Date, @CreatedAt)";
        await _dbConnection.ExecuteAsync(sql, entityModel);
        return entityModel;
    }

    public async Task<Expense?> DeleteEntity(Guid entityId)
    {
        var sql = "DELETE FROM Expenses WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = entityId });
        return result > 0 ? new Expense() : null; 
    }

    public async Task<List<Expense>> GetAllEntities()
    {
        var sql = "SELECT * FROM Expenses";
        var expenses = await _dbConnection.QueryAsync<Expense>(sql);
        return expenses.ToList();
    }

    public async Task<Expense?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Expenses WHERE Id = @Id";
        var expense = await _dbConnection.QueryFirstOrDefaultAsync<Expense>(sql, new { Id = entityId });
        return expense;
    }

    public async Task<Expense?> UpdateEntity(Guid entityId, IDto<Expense> entityDto)
    {
        var updatedExpense = entityDto.GetDto();
        updatedExpense.Id = entityId; 

        var sql = @"UPDATE Expenses 
                       SET Amount = @Amount, Description = @Description, Category = @Category, Date = @Date 
                       WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, updatedExpense);
        return result > 0 ? updatedExpense : null;
    }
}