namespace ExpenseTracker.Dtos.MetricDto;

public class GoalProgressDto
{
    public Guid GoalId { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public decimal ProgressPercentage { get; set; }
    public decimal RemainingAmount { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsMilestoneAchieved { get; set; }
}