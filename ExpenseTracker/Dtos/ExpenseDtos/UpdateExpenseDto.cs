using ExpenseTracker.Domain;

namespace ExpenseTracker.Dtos.ExpenseDtos;

public class UpdateExpenseDto : IDto<Expense>
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }

    public UpdateExpenseDto(decimal amount, string description, string category, DateTime date)
    {
        Amount = amount;
        Description = description;
        Category = category;
        Date = date;
    }

    public Expense GetDto()
    {
        return new Expense(Guid.NewGuid(), Amount, Description, Category, Date); 
    }
}