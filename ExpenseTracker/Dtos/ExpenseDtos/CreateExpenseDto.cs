using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.ExpenseDtos;

public class CreateExpenseDto : IInDto<Expense>
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }

    public Expense GetEntity(Expense? entity)
    {
        return new Expense
        {
            Id = Guid.NewGuid(),
            UserId = UserId, 
            Amount = Amount,
            Description = Description,
            Category = Category,
            Date = Date,
        };
    }
}