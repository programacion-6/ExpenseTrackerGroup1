using ExpenseTracker.Domain;

namespace ExpenseTracker.Dtos.ExpenseDtos;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }

    public ExpenseDto(Guid id, Guid userId, decimal amount, string description, string category, DateTime date, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Amount = amount;
        Description = description;
        Category = category;
        Date = date;
        CreatedAt = createdAt;
    }

    
    public static ExpenseDto FromExpense(Expense expense)
    {
        return new ExpenseDto(expense.Id, expense.UserId, expense.Amount, expense.Description, expense.Category, expense.Date, expense.CreatedAt);
    }
}