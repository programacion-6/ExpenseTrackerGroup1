using System;
using Dapper;
using ExpenseTracker.Persistence.Database.Interface;

public class IncomeRepository : IIncomeRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public IncomeRepository(IDbConnectionFactory dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Income> CreateEntity(Income incomeModel)
    {
        var sql = "INSERT INTO Incomes (UserId, Amount, Source, Date, CreatedAt) " +
                  "VALUES (@UserId, @Amount, @Source, @Date, @CreatedAt) RETURNING Id;";
        incomeModel.CreatedAt = DateTime.UtcNow;
        using var connection = await _dbConnection.CreateConnectionAsync();
        var id = await connection.ExecuteScalarAsync<Guid>(sql, incomeModel);
        incomeModel.Id = id;
        return incomeModel;
    }

    public async Task<Income?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Incomes WHERE Id = @Id;";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Income>(sql, new { Id = entityId });
    }

    public async Task<bool> UpdateEntity(Guid entityId, Income entity)
{
    var sql = @"UPDATE Incomes 
                    SET Amount = @Amount, 
                        Source = @Source, 
                        Date = @Date 
                    WHERE Id = @Id;";
    using var connection = await _dbConnection.CreateConnectionAsync();
    var affectedRows = await connection.ExecuteAsync(sql, new
    {
        Id = entityId,
        Amount = entity.Amount,
        Source = entity.Source,
        Date = entity.Date
    });
    return affectedRows > 0;
}

  public async Task<Income?> DeleteEntity(Guid entityId)
{
    var sql = "DELETE FROM Incomes WHERE Id = @Id RETURNING *;";
    using var connection = await _dbConnection.CreateConnectionAsync();
    var deletedIncome = await connection.QuerySingleOrDefaultAsync<Income>(sql, new { Id = entityId });
    return deletedIncome;
}


    public async Task<IEnumerable<Income>> GetIncomesByUserId(Guid userId)
    {
        var sql = "SELECT * FROM Incomes WHERE UserId = @UserId;";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Income>(sql, new { UserId = userId });
    }

    public async Task<IEnumerable<Income>> GetAllEntities()
    {
        var sql = "SELECT * FROM Incomes;";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Income>(sql);
    }
}
