public class Budget
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal BudgetAmount { get; private set; }
    public string Month { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Budget(Guid userId, decimal budgetAmount, string month)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        BudgetAmount = budgetAmount;
        Month = month;
        CreatedAt = DateTime.UtcNow;
    }

    public bool IsExceeded(decimal totalExpenses)
    {
        return totalExpenses > BudgetAmount;
    }

    public void UpdateBudget(decimal newBudgetAmount)
    {
        BudgetAmount = newBudgetAmount;
    }
}
