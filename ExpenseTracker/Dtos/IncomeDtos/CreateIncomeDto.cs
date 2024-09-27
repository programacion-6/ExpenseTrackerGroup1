using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.IncomeDtos;

public class CreateIncomeDto : IInDto<Income>
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }

    public Income GetEntity(Income? entity)
    {
        return new Income(UserId, Amount, Source, Date)
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };
    }
}
