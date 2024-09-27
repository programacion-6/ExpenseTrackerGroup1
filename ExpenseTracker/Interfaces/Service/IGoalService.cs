using ExpenseTracker.Dtos.GoalDtos;

namespace ExpenseTracker.Interfaces.Service
{
    public interface IGoalService
    {
        Task<GoalDto> CreateGoalAsync(Guid userId, CreateGoalDto goalDto);
        Task<GoalDto?> GetGoalByIdAsync(Guid id);
        Task<IEnumerable<GoalDto>> GetGoalsByUserIdAsync(Guid userId);
        Task<bool> UpdateGoalAsync(Guid id, UpdateGoalDto goalDto);
    }
}