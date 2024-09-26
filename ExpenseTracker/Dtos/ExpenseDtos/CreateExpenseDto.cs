using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.ExpenseDtos;

public class CreateExpenseDto : IDto<CreateExpenseDto>
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }

    public CreateExpenseDto(decimal amount, string description, string category, DateTime date)
    {
        Amount = amount;
        Description = description;
        Category = category;
        Date = date;
    }

    public CreateExpenseDto GetEntity(CreateExpenseDto entity)
    {
        return this;
    }
}