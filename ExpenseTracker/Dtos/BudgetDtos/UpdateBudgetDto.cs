using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.BudgetDtos;

public class UpdateBudgetDto : IInDto<Budget>
{
    public decimal BudgetAmount { get; set; }

    public Budget GetEntity(Budget? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        return new Budget
        {
            UserId = entity.UserId,
            BudgetAmount = BudgetAmount,
            Month = entity.Month,
            Id = entity.Id
        };
    }
}