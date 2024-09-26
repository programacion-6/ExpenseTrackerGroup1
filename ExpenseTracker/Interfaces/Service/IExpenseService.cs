using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.ExpenseDtos;
using ExpenseTracker.Dtos.UserDtos;

namespace ExpenseTracker.Interfaces.Service;

public interface IExpenseService
{
    Task<Expense?> GetExpenseById(Guid expenseId);
    Task<List<Expense>> GetAllExpenses();
    Task<Expense?> CreateExpense(Expense expense);
    Task<Expense?> UpdateExpense(Guid expenseId, UpdateExpenseDto expenseUpdateDto);
    Task<Expense?> DeleteExpense(Guid expenseId);
    
}