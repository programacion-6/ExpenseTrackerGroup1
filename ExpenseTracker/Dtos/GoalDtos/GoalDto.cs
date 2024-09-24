namespace ExpenseTracker.Dtos.GoalDtos;

public class GoalDto
{
    public Guid Id { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsComplete { get; set; }

    public GoalDto(Guid id, decimal goalAmount, decimal currentAmount, DateTime deadline)
    {
        Id = id;
        GoalAmount = goalAmount;
        CurrentAmount = currentAmount;
        Deadline = deadline;
        IsComplete = currentAmount >= goalAmount;
    }
}