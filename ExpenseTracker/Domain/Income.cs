public class Income : IEntity
{
    public Guid Id { get;  set; }
    public Guid UserId { get;  set; }
    public decimal Amount { get;  set; }
    public string Source { get;  set; }
    public DateTime Date { get;  set; }
    public DateTime CreatedAt { get;  set; }

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
