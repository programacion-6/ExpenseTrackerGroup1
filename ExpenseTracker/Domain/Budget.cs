namespace ExpenseTracker.Domain;

public class Budget : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal BudgetAmount { get; set; }
    public string Month { get; set; }

    public Budget() { }

    public Budget(Guid id, Guid userId, decimal budgetAmount, string month)
    {
        Id = id;
        UserId = userId;
        BudgetAmount = budgetAmount;
        Month = month;
    }
}