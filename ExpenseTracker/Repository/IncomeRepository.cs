using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Npgsql;

public class IncomeRepository
{
     private readonly IDbConnection _dbConnection;

    public IncomeRepository(string connectionString)
    {
        _dbConnection = new NpgsqlConnection(connectionString);
    }

    public async Task<Income> CreateEntity(Income incomeModel)
    {
        var sql = "INSERT INTO Incomes (UserId, Amount, Source, Date, CreatedAt) " +
                  "VALUES (@UserId, @Amount, @Source, @Date, @CreatedAt) RETURNING Id;";
        incomeModel.CreatedAt = DateTime.UtcNow;
        var id = await _dbConnection.ExecuteScalarAsync<Guid>(sql, incomeModel);
        incomeModel.Id = id;
        return incomeModel;
    }

    public async Task<Income?> ReadEntity(Guid entityId)
    {
        var sql = "SELECT * FROM Incomes WHERE Id = @Id;";
        return await _dbConnection.QuerySingleOrDefaultAsync<Income>(sql, new { Id = entityId });
    }

    public async Task<Income?> UpdateEntity(Guid entityId, UpdateIncomeDto entityDto)
    {
        var sql = "UPDATE Incomes SET Amount = @Amount, Source = @Source, Date = @Date " +
                  "WHERE Id = @Id RETURNING *;";
        var updatedIncome = await _dbConnection.QuerySingleOrDefaultAsync<Income>(sql, new
        {
            Id = entityId,
            Amount = entityDto.Amount,
            Source = entityDto.Source,
            Date = entityDto.Date
        });
        return updatedIncome;
    }

    public async Task<Income?> DeleteEntity(Guid entityId)
    {
        var sql = "DELETE FROM Incomes WHERE Id = @Id RETURNING *;";
        return await _dbConnection.QuerySingleOrDefaultAsync<Income>(sql, new { Id = entityId });
    }
}
