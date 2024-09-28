using ExpenseTracker.Dtos.MetricDto;

namespace ExpenseTracker.Interfaces;

public interface IMetricRepository
{
    Task<IEnumerable<GoalProgressDto>> GetGoalsWithProgress(Guid userId);
    Task<decimal> GetTotalExpensesForMonth(Guid userId, DateTime firstDayOfMonth);
    Task<decimal> GetTotalIncomeForMonth(Guid userId, DateTime firstDayOfMonth);
    Task<decimal> GetBudgetForMonth(Guid userId, DateTime firstDayOfMonth);
    Task<ExpenseInsightsDto> GetExpenseInsights(Guid userId);
}