using ExpenseTracker.Domain;

namespace ExpenseTracker.Dtos.BudgetDtos;

public class UpdateBudgetDto : IDto<UpdateBudgetDto>
{
    public decimal BudgetAmount { get; set; }

    UpdateBudgetDto(decimal budgetAmount)
    {
        BudgetAmount = budgetAmount;
    }

    public UpdateBudgetDto GetDto()
    {
        return this;
    }
}