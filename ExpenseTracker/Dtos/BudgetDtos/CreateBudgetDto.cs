namespace ExpenseTracker.Dtos.BudgetDtos;

public class CreateBudgetDto
{
    public Guid UserId { get; set; }
    public decimal BudgetAmount { get; set; }
    public string Month { get; set; }

    public CreateBudgetDto(Guid userId, decimal budgetAmount, string month)
    {
        UserId = userId;
        BudgetAmount = budgetAmount;
        Month = month;
    }

    public CreateBudgetDto GetDto()
    {
        return this;
    }
}