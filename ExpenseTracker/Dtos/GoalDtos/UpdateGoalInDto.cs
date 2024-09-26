using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.GoalDtos;

public class UpdateGoalInDto : IInDto<Goal>
{
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CurrentAmount { get; set; }

    public Goal GetEntity(Goal entity)
    {
        return new Goal
        {
            GoalAmount = GoalAmount,
            Deadline = Deadline,
            CurrentAmount = CurrentAmount
        };
    }
}