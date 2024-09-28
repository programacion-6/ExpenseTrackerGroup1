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
        var sql = @"INSERT INTO Income (Id, UserId, Amount, Source, Date, CreatedAt) 
                        VALUES (@Id, @UserId, @Amount, @Source, @Date, @CreatedAt)";
        using var connection = await _dbConnection.CreateConnectionAsync();
        await connection.ExecuteAsync(sql, incomeModel);
        return incomeModel;
    }

    public async Task<Income?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Income WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QuerySingleOrDefaultAsync<Income>(sql, new { Id = entityId });
    }

    public async Task<bool> UpdateEntity(Guid entityId, Income entity)
{
    var sql = @"UPDATE Income 
                        SET Amount = @Amount, 
                            Source = @Source, 
                            Date = @Date 
                        WHERE Id = @Id";
            using var connection = await _dbConnection.CreateConnectionAsync();
            var affectedRows = await connection.ExecuteAsync(sql, entity);
            return affectedRows > 0;
}

  public async Task<Income?> DeleteEntity(Guid entityId)
{
    var sql = "DELETE FROM Income WHERE Id = @Id";
    using var connection = await _dbConnection.CreateConnectionAsync();
    var rowsAffected = await connection.ExecuteAsync(sql, new { Id = entityId });
    return null;
}


    public async Task<IEnumerable<Income>> GetIncomesByUserId(Guid userId)
    {
        var sql = "SELECT * FROM Income WHERE UserId = @UserId";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return await connection.QueryAsync<Income>(sql, new { UserId = userId });
    }

    public async Task<List<Income>> GetAllEntities()
    {
        var sql = "SELECT * FROM Income";
        using var connection = await _dbConnection.CreateConnectionAsync();
        return (await connection.QueryAsync<Income>(sql)).AsList();
    }
}
