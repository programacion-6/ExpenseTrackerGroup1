public class Income : IEntity
{
    public Guid Id { get;  set; }
    public Guid UserId { get;  set; }
    public decimal Amount { get;  set; }
    public string Source { get;  set; }
    public DateTime Date { get;  set; }
    public DateTime CreatedAt { get;  set; }
}
