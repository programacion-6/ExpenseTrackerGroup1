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

    public Expense(Guid userId, decimal amount, string description, string category, DateTime date)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Description = description;
        Category = category;
        Date = date;
        CreatedAt = DateTime.UtcNow;
    }

    public Expense()
    {
    }

    public void UpdateDetails(decimal amount, string category, string description)
    {
        Amount = amount;
        Category = category;
        Description = description;
    }
}
