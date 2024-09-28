namespace ExpenseTracker.Dtos.MetricDto;

public class MonthlySummaryDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal RemainingBudget { get; set; }
    public decimal BudgetAmount { get; set; }
}