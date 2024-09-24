namespace ExpenseTracker.Dtos.GoalDtos;

public class UpdateGoalDto : IDto<Goal>
{
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CurrentAmount { get; set; }

    public UpdateGoalDto(decimal goalAmount, DateTime deadline, decimal currentAmount)
    {
        GoalAmount = goalAmount;
        Deadline = deadline;
        CurrentAmount = currentAmount;
    }

    public Goal GetDto()
    {
        return new Goal
        {
            GoalAmount = GoalAmount,
            Deadline = Deadline,
            CurrentAmount = CurrentAmount
        };
    }
}