using System.Data;
using Dapper;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Repository;

public class GoalRepository : IGoalRepository
{
    private readonly IDbConnection _dbConnection;

    public GoalRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Goal> CreateEntity(Goal entityModel)
    {
        var sql = @"INSERT INTO Goal (Id, UserId, GoalAmount, Deadline, CurrentAmount) 
                        VALUES (@Id, @UserId, @GoalAmount, @Deadline, @CurrentAmount)";
        await _dbConnection.ExecuteAsync(sql, entityModel);
        return entityModel;
    }

    public async Task<Goal?> ReadEntity(Guid entityId)
    {
        var sql = @"SELECT * FROM Goal WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Goal>(sql, new { Id = entityId });
    }

    public async Task<IEnumerable<Goal>> GetGoalsByUserId(Guid userId)
    {
        var sql = @"SELECT * FROM Goal WHERE UserId = @UserId";
        return await _dbConnection.QueryAsync<Goal>(sql, new { UserId = userId });
    }

    public async Task<Goal?> UpdateEntity(Guid entityId, IDto<Goal> entityDto)
    {
        var existingGoal = await ReadEntity(entityId);
        if (existingGoal == null)
        {
            return null; 
        }
        var updatedGoal = entityDto.GetDto();
        var sql = @"UPDATE Goal 
                        SET GoalAmount = @GoalAmount, 
                            Deadline = @Deadline, 
                            CurrentAmount = @CurrentAmount 
                        WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(sql, new
        {
            Id = entityId,
            GoalAmount = updatedGoal.GoalAmount,
            Deadline = updatedGoal.Deadline,
            CurrentAmount = updatedGoal.CurrentAmount
        });
        return await ReadEntity(entityId);
    }
}