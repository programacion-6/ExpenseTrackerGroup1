namespace ExpenseTracker.Dtos.MetricDto;

public class ExpenseInsightsDto
{
    public string HighestSpendingCategory { get; set; }
    public decimal HighestSpendingCategoryAmount { get; set; }
    public string MostExpensiveMonth { get; set; }
    public decimal MostExpensiveMonthAmount { get; set; }
}