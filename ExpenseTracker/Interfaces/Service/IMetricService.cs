using ExpenseTracker.Dtos.MetricDto;

namespace ExpenseTracker.Interfaces.Service;

public interface IMetricService
{
    Task<MonthlySummaryDto> GetMonthlySummary(Guid userId, DateTime month);
    Task<IEnumerable<GoalProgressDto>> GetUserGoalsWithProgress(Guid userId);
    Task<ExpenseInsightsDto> GetExpenseInsights(Guid userId);
}