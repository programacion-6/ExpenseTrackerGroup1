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

    private readonly IUserRepository _userRepository;


    public ExpenseService(IExpenseRepository expenseRepository, IUserRepository userRepository)
    {
         _expenseRepository = expenseRepository;
         _userRepository = userRepository;
    }

    public async Task<Expense> CreateExpenseAsync(Guid userId, CreateExpenseDto expenseDto)
    {
        if (expenseDto == null)
            throw new ArgumentNullException(nameof(expenseDto));
        
        var user = await _userRepository.ReadEntity(userId);
        if (user == null)
                throw new ArgumentException("User does not exist.");
        
        var expense = expenseDto.GetEntity(new Expense{UserId = user.Id});
        var createdExpense = await _expenseRepository.CreateEntity(expense);
        return createdExpense;
    }

    public  async Task<Expense?> DeleteExpenseAsync(Guid expenseId)
    {
        if (expenseId == Guid.Empty)
            throw new ArgumentException("Expense ID cannot be empty.", nameof(expenseId));
        
        var existingExpense = await _expenseRepository.ReadEntity(expenseId);

        if (existingExpense == null)
            throw new KeyNotFoundException($"Expense with ID {expenseId} not found.");

       
        return await _expenseRepository.DeleteEntity(expenseId);
    }

    public  async Task<List<Expense>> GetAllExpensesAsync()
    {
        return await _expenseRepository.GetAllEntities(); 
    }

    public async Task<ExpenseDto?> GetExpenseByIdAsync(Guid expenseId)
    {
        if (expenseId == Guid.Empty)
            throw new ArgumentException("Expense ID cannot be empty.", nameof(expenseId));
        
        var expense = await _expenseRepository.ReadEntity(expenseId);
        if (expense == null)
            throw new KeyNotFoundException($"Expense with ID {expenseId} not found.");

        return new ExpenseDto().GetDto(expense);
    }

    public async Task<bool> UpdateExpenseAsync(Guid expenseId, UpdateExpenseDto expenseDto)
    {   
        if (expenseId == Guid.Empty)
                throw new ArgumentException("Goal ID cannot be empty.");
        
        if (expenseDto == null)
                throw new ArgumentNullException(nameof(expenseDto));
        
        var existingExpense = await _expenseRepository.ReadEntity(expenseId);
        
        if (existingExpense == null)
            throw new ArgumentException("Expense does not exist.", nameof(expenseId));

        var updatedExpense = expenseDto.GetEntity(existingExpense);
        return await _expenseRepository.UpdateEntity(expenseId, updatedExpense);
    }
}