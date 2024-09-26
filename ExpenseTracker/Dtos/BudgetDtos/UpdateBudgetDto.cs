using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

public class UpdateBudgetDto : IDto<Budget>
{
    public decimal BudgetAmount { get; set; }

    public UpdateBudgetDto(decimal budgetAmount)
    {
        BudgetAmount = budgetAmount;
    }

    public Budget GetEntity(Budget entity)
    {
        return new Budget
        {
            BudgetAmount = this.BudgetAmount,
        };
    }
}