public class Expense
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }

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

    public void UpdateDetails(decimal amount, string category, string description)
    {
        Amount = amount;
        Category = category;
        Description = description;
    }
}
