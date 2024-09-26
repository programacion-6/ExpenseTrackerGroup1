using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.BudgetDtos;

public class CreateBudgetDto : IInDto<Budget>
{
    public Guid UserId { get; set; }
    public decimal BudgetAmount { get; set; }
    public DateTime Month { get; set; }
    
    public Budget GetEntity(Budget? entity)
    {
        return new Budget
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            Month = new DateTime(Month.Year, Month.Month, 1),
            BudgetAmount = BudgetAmount,
        };
    }
}