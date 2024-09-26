using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.GoalDtos;

public class CreateGoalDto : IInDto<Goal>
{
    public Guid UserId { get; set; }

    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }

    public Goal GetEntity(Goal? entity)
    {
        return new Goal
        {
            Id = Guid.NewGuid(),
            GoalAmount = GoalAmount ,
            CurrentAmount = 0,
            UserId = UserId,
            Deadline = Deadline
        };
    }
}