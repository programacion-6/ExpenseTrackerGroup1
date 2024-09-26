using ExpenseTracker.Domain;

public class UpdateBudgetDto : IDto<Budget>
{
    public decimal BudgetAmount { get; set; }

    public UpdateBudgetDto(decimal budgetAmount)
    {
        BudgetAmount = budgetAmount;
    }

    public Budget GetDto()
    {
        return new Budget
        {
            BudgetAmount = this.BudgetAmount,
        };
    }
}