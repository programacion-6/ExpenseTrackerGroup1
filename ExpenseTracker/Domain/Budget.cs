namespace ExpenseTracker.Domain;

public class Budget : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal BudgetAmount { get; set; }
    public DateTime Month { get; set; }
}