using ExpenseTracker.Interfaces.Service;
namespace ExpenseTracker.Dtos.IncomeDtos;

public class IncomeDto : IOutDto<IncomeDto, Income>
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }

    public IncomeDto GetDto(Income entity)
    {
        return new IncomeDto
        {
            UserId = entity.UserId,
            Amount = entity.Amount,
            Source = entity.Source,
            Date = entity.Date,
            CreatedAt = entity.CreatedAt,
        };
    }
}
