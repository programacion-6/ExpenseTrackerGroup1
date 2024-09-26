using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.GoalDtos;

public class UpdateGoalDto : IInDto<Goal>
{
    public decimal? GoalAmount { get; set; }
    public DateTime? Deadline { get; set; }
    public decimal? CurrentAmount { get; set; }

    public Goal GetEntity(Goal? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        return new Goal
        {
            Id = entity.Id,
            GoalAmount = GoalAmount ?? entity.GoalAmount,
            Deadline = Deadline ?? entity.Deadline,
            CurrentAmount = CurrentAmount ?? entity.CurrentAmount,
            UserId = entity.UserId
        };
    }
}