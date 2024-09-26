namespace ExpenseTracker.Domain;

public class Expense : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }

}
