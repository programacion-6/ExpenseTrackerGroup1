public class Goal : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
}
