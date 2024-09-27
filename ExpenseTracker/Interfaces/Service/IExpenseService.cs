using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.ExpenseDtos;
using ExpenseTracker.Dtos.UserDtos;

namespace ExpenseTracker.Interfaces.Service;

public interface IExpenseService
{
    Task<ExpenseDto?> GetExpenseByIdAsync(Guid expenseId);
    Task<List<Expense>> GetAllExpensesAsync();
    Task<ExpenseDto> CreateExpenseAsync(CreateExpenseDto expenseDto);
    Task<bool> UpdateExpenseAsync(Guid expenseId, UpdateExpenseDto expenseDto);
    Task<Expense?> DeleteExpenseAsync(Guid expenseId);
    
}