using ExpenseTracker.Dtos.MetricDto;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Services;

public class MetricService : IMetricService
{
    private readonly IMetricRepository _metricRepository;

    public MetricService(IMetricRepository metricRepository)
    {
        _metricRepository = metricRepository;
    }

    public async Task<MonthlySummaryDto> GetMonthlySummary(Guid userId, DateTime month)
    {
        var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
        var totalExpenses = await _metricRepository.GetTotalExpensesForMonth(userId, firstDayOfMonth);
        var totalIncome = await _metricRepository.GetTotalIncomeForMonth(userId, firstDayOfMonth);
        var budgetAmount = await _metricRepository.GetBudgetForMonth(userId, firstDayOfMonth);
        var remainingBudget = budgetAmount - totalExpenses;
        var monthlySummary = new MonthlySummaryDto
        {
            TotalIncome = totalIncome,
            TotalExpenses = totalExpenses,
            RemainingBudget = remainingBudget,
            BudgetAmount = budgetAmount
        };
        return monthlySummary;
    }
    
    public async Task<IEnumerable<GoalProgressDto>> GetUserGoalsWithProgress(Guid userId)
    {
        return await _metricRepository.GetGoalsWithProgress(userId);
    }
    
    public async Task<ExpenseInsightsDto> GetExpenseInsights(Guid userId)
    {
        return await _metricRepository.GetExpenseInsights(userId);
    }
}