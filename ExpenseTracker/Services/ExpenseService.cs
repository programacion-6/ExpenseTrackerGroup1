using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.ExpenseDtos;
using ExpenseTracker.Dtos.UserDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;
using ExpenseTracker.Utils.ParamValidator;

namespace ExpenseTracker.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
         _expenseRepository = expenseRepository;
    }

    public async Task<Expense?> CreateExpense(Expense expense)
    {
        throw new NotImplementedException();
    }

    public async Task<Expense?> DeleteExpense(Guid expenseId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Expense>> GetAllExpenses()
    {
        throw new NotImplementedException();
    }

    public async Task<ExpenseDto?> GetExpenseById(Guid expenseId)
    {
        if (expenseId == Guid.Empty)
                throw new ArgumentException("Expense ID cannot be empty.");

        var expense = await _expenseRepository.ReadEntity(expenseId);
        if(expense == null)
        {
            throw new KeyNotFoundException($"Expense with ID {expenseId} not found.");
        }
        
        return new ExpenseDto.GetDto(expense);
    }

    public Task<Expense?> UpdateExpense(Guid expenseId, UpdateExpenseDto expenseUpdateDto)
    {
        throw new NotImplementedException();
    }
}