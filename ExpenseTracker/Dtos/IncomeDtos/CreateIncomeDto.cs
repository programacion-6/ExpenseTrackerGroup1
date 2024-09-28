using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.IncomeDtos;

public class CreateIncomeDto : IInDto<Income>
{
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }

    public Income GetEntity(Income? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        return new Income()
        {
            Id = Guid.NewGuid(),
            UserId = entity.UserId,
            Amount = Amount,
            Source = Source,
            Date = Date,
        };
    }
}
