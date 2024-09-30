using Dapper;
using ExpenseTracker.Dtos.MetricDto;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Persistence.Database.Interface;

namespace ExpenseTracker.Repository
{
    public class MetricRepository : IMetricRepository
    {
        private readonly IDbConnectionFactory _dbConnection;

        public MetricRepository(IDbConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<GoalProgressDto>> GetGoalsWithProgress(Guid userId)
        {
            var sql = @"SELECT Id AS GoalId, GoalAmount, CurrentAmount, Deadline
                        FROM Goal 
                        WHERE UserId = @UserId";
            using var connection = await _dbConnection.CreateConnectionAsync();
            var goals = await connection.QueryAsync<GoalProgressDto>(sql, new { UserId = userId });

            var goalProgressList = goals.Select(goal => new GoalProgressDto
            {
                GoalId = goal.GoalId,
                GoalAmount = goal.GoalAmount,
                CurrentAmount = goal.CurrentAmount,
                ProgressPercentage = CalculateProgressPercentage(goal.CurrentAmount, goal.GoalAmount),
                RemainingAmount = goal.GoalAmount - goal.CurrentAmount,
                Deadline = goal.Deadline,
                IsMilestoneAchieved = CalculateMilestoneAchieved(goal.CurrentAmount, goal.GoalAmount)
            });

            return goalProgressList;
        }

        public async Task<ExpenseInsightsDto> GetExpenseInsights(Guid userId)
        {
            using var connection = await _dbConnection.CreateConnectionAsync();
            var highestSpendingCategorySql = @"
                SELECT Category, SUM(Amount) AS TotalAmount
                FROM Expense
                WHERE UserId = @UserId
                GROUP BY Category
                ORDER BY TotalAmount DESC
                LIMIT 1";
            var highestSpendingCategory = await connection.QueryFirstOrDefaultAsync<(string Category, decimal TotalAmount)>(highestSpendingCategorySql, new { UserId = userId });
            var mostExpensiveMonthSql = @"
                SELECT TO_CHAR(Date, 'YYYY-MM') AS Month, SUM(Amount) AS TotalAmount
                FROM Expense
                WHERE UserId = @UserId
                GROUP BY TO_CHAR(Date, 'YYYY-MM')
                ORDER BY TotalAmount DESC
                LIMIT 1";
            var mostExpensiveMonth = await connection.QueryFirstOrDefaultAsync<(string Month, decimal TotalAmount)>(mostExpensiveMonthSql, new { UserId = userId });
            var expenseInsights = new ExpenseInsightsDto
            {
                HighestSpendingCategory = highestSpendingCategory.Category,
                HighestSpendingCategoryAmount = highestSpendingCategory.TotalAmount,
                MostExpensiveMonth = mostExpensiveMonth.Month,
                MostExpensiveMonthAmount = mostExpensiveMonth.TotalAmount
            };

            return expenseInsights;
        }
        public async Task<decimal> GetTotalExpensesForMonth(Guid userId, DateTime firstDayOfMonth)
        {
            var sql = @"SELECT COALESCE(SUM(Amount), 0) 
                        FROM Expense 
                        WHERE UserId = @UserId AND 
                              Date >= @FirstDayOfMonth AND 
                              Date < @FirstDayOfMonth + INTERVAL '1 MONTH'";
            using var connection = await _dbConnection.CreateConnectionAsync();
            return await connection.ExecuteScalarAsync<decimal>(sql, new { UserId = userId, FirstDayOfMonth = firstDayOfMonth });
        }

        public async Task<decimal> GetTotalIncomeForMonth(Guid userId, DateTime firstDayOfMonth)
        {
            var sql = @"SELECT COALESCE(SUM(Amount), 0) 
                        FROM Income 
                        WHERE UserId = @UserId AND 
                              Date >= @FirstDayOfMonth AND 
                              Date < @FirstDayOfMonth + INTERVAL '1 MONTH'";
            using var connection = await _dbConnection.CreateConnectionAsync();
            return await connection.ExecuteScalarAsync<decimal>(sql, new { UserId = userId, FirstDayOfMonth = firstDayOfMonth });
        }

        public async Task<decimal> GetBudgetForMonth(Guid userId, DateTime firstDayOfMonth)
        {
            var sql = @"SELECT COALESCE(BudgetAmount, 0) 
                        FROM Budget 
                        WHERE UserId = @UserId AND Month = @FirstDayOfMonth";
            using var connection = await _dbConnection.CreateConnectionAsync();
            return await connection.ExecuteScalarAsync<decimal>(sql, new { UserId = userId, FirstDayOfMonth = firstDayOfMonth });
        }
        
        private decimal CalculateProgressPercentage(decimal currentAmount, decimal goalAmount)
        {
            return goalAmount > 0 ? Math.Round((currentAmount / goalAmount) * 100, 2) : 0;
        }

        private bool CalculateMilestoneAchieved(decimal currentAmount, decimal goalAmount)
        {
            return goalAmount > 0 && currentAmount >= goalAmount * (decimal)0.5;
        }
    }
}
