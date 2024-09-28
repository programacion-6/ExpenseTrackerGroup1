using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.IncomeDtos;

public class UpdateIncomeInDto : IInDto<Income>
{
    public decimal? Amount { get; set; }
    public string? Source { get; set; }
    public DateTime? Date { get; set; }

    public Income GetEntity(Income? entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        
        return new Income()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Amount = Amount ?? entity.Amount,
            Source = Source ?? entity.Source,
            Date = Date ?? entity.Date,
            CreatedAt = entity.CreatedAt
        };
    }
}
