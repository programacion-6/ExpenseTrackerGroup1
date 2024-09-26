using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.BudgetDtos;

namespace ExpenseTracker.Interfaces.Service
{
    public interface IBudgetService
    {
        Task<Budget?> GetMonthlyBudget(Guid userId, DateTime month);
        Task<Budget?> GetCurrentMonthBudget(Guid userId);
        Task<Budget?> ReadEntity(Guid entityId);
        Task<Budget> CreateEntity(CreateBudgetDto budgetDto);
        Task<bool> UpdateEntity(Guid entityId, UpdateBudgetDto budgetDto);
    }
}