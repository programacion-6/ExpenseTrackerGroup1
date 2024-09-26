using System.Data;
using Dapper;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Persistence.Database.Interface;

namespace ExpenseTracker.Repository;

public class GoalRepository : IGoalRepository
{
    private readonly IDbConnectionFactory _dbConnection;

    public GoalRepository(IDbConnectionFactory dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Goal> CreateEntity(Goal entityModel)
    {
        var sql = @"INSERT INTO Goal (Id, UserId, GoalAmount, Deadline, CurrentAmount) 
                        VALUES (@Id, @UserId, @GoalAmount, @Deadline, @CurrentAmount)";
        using var connection = await _dbConnection.CreateConnectionAsync();

        await connection.ExecuteAsync(sql, entityModel);
        return entityModel;
    }

    public async Task<Goal?> ReadEntity(Guid entityId)
    {
        var sql = @"SELECT * FROM Goal WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();

        return await connection.QueryFirstOrDefaultAsync<Goal>(sql, new { Id = entityId });
    }

    public async Task<IEnumerable<Goal>> GetGoalsByUserId(Guid userId)
    {
        var sql = @"SELECT * FROM Goal WHERE UserId = @UserId";
        using var connection = await _dbConnection.CreateConnectionAsync();

        return await connection.QueryAsync<Goal>(sql, new { UserId = userId });
        
    }

    public async Task<Goal?> UpdateEntity(Guid entityId, IDto<Goal> entityDto)
    {
        var existingGoal = await ReadEntity(entityId);
        if (existingGoal == null)
        {
            return null; 
        }
        var updatedGoal = entityDto.GetEntity(new Goal{Id = entityId});
        var sql = @"UPDATE Goal 
                        SET GoalAmount = @GoalAmount, 
                            Deadline = @Deadline, 
                            CurrentAmount = @CurrentAmount 
                        WHERE Id = @Id";
        using var connection = await _dbConnection.CreateConnectionAsync();

        await connection.ExecuteAsync(sql, new
        {
            Id = entityId,
            GoalAmount = updatedGoal.GoalAmount,
            Deadline = updatedGoal.Deadline,
            CurrentAmount = updatedGoal.CurrentAmount
        });
        return await ReadEntity(entityId);
    }
}