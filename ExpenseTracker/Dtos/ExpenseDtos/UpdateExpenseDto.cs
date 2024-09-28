using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.ExpenseDtos;

public class UpdateExpenseDto : IInDto<Expense>
{
    public decimal? Amount { get; set; }
    public string? Description { get; set; }
    public string? Category { get; set; }
    public DateTime? Date { get; set; }

    public Expense GetEntity(Expense? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        return new Expense
        {
            Id = entity.Id,
            Amount = Amount ?? entity.Amount,
            Description = Description ?? entity.Description,
            Category = Category ?? entity.Category,
            Date = Date ?? entity.Date,
            CreatedAt = entity.CreatedAt
        };
    }
}