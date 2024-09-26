using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Dtos.BudgetDtos;

public class BudgetDto : IOutDto<BudgetDto, Budget>
{
    public Guid UserId { get; set; }
    public decimal BudgetAmount { get; set; }
    public DateTime Month { get; set; }
    
    public BudgetDto GetDto(Budget entity)
    {
        return new BudgetDto
        {
            UserId = entity.UserId,
            BudgetAmount = entity.BudgetAmount,
            Month = entity.Month
        };
    }
}