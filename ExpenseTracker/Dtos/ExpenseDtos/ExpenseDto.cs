using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Dtos.ExpenseDtos;

public class ExpenseDto : IOutDto<ExpenseDto, Expense>
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }

    public ExpenseDto GetDto(Expense entity)
    {
        return new ExpenseDto
        {
            UserId = entity.UserId,
            Amount = entity.Amount,
            Description = entity.Description,
            Category = entity.Category,
            Date = entity.Date,
            CreatedAt = entity.CreatedAt,
        };
    }
}
