public class Income : IEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Source { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Income(Guid userId, decimal amount, string source, DateTime date)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Source = source;
        Date = date;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateIncome(decimal amount, string source)
    {
        Amount = amount;
        Source = source;
    }
}
