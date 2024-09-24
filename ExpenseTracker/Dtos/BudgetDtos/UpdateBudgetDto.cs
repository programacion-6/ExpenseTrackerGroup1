namespace ExpenseTracker.Dtos.BudgetDtos;

public class UpdateBudgetDto
{
    public decimal BudgetAmount { get; set; }

    public UpdateBudgetDto(decimal budgetAmount)
    {
        BudgetAmount = budgetAmount;
    }

    public UpdateBudgetDto GetDto()
    {
        return this;
    }
}