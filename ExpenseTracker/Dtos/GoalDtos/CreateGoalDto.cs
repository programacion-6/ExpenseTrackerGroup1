using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.GoalDtos;

public class CreateGoalDto : IInDto<Goal>
{
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }

    public Goal GetEntity(Goal? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        return new Goal
        {
            Id = Guid.NewGuid(),
            GoalAmount = GoalAmount ,
            CurrentAmount = 0,
            UserId = entity.UserId,
            Deadline = Deadline
        };
    }
}