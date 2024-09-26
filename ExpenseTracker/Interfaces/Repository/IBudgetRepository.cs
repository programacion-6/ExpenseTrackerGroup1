using ExpenseTracker.Domain;

namespace ExpenseTracker.Interfaces;

public interface IBudgetRepository : IReadOperation<Budget>, ICreateOperation<Budget>, IUpdateOperation<Budget>
{
    public Task<Budget?> GetMonthlyBudget(Guid userId, DateTime month);
    public Task<Budget?> GetCurrentMonthBudget(Guid userId);
}