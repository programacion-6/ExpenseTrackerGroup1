public class Goal
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CurrentAmount { get; set; } = 0; // Inicialmente 0
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; }

    public void AddAmount(decimal amount)
    {
        if (amount > 0)
        {
            CurrentAmount += amount;
        }
    }

    public bool IsGoalAchieved()
    {
        return CurrentAmount >= GoalAmount;
    }
}
