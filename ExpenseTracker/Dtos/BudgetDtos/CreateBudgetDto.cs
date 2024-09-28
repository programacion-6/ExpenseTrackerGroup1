using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.BudgetDtos;

public class CreateBudgetDto : IInDto<Budget>
{
    public decimal BudgetAmount { get; set; }
    public DateTime Month { get; set; }
    
    public Budget GetEntity(Budget? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        return new Budget
        {
            Id = Guid.NewGuid(),
            UserId = entity.UserId,
            Month = new DateTime(Month.Year, Month.Month, 1),
            BudgetAmount = BudgetAmount,
        };
    }
}