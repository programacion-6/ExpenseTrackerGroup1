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
        
        return new Income(
            entity.UserId,
            Amount ?? entity.Amount,
            Source ?? entity.Source,
            Date ?? entity.Date)
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt
        };
    }
}
