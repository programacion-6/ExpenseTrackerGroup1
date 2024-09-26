using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.IncomeDtos;

public class UpdateIncomeInDto : IInDto<UpdateIncomeInDto>
{
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }

    public UpdateIncomeInDto(decimal amount, string source, DateTime date)
    {
        Source = source;
        Amount = amount;
        Date = date;
    }
    
    public UpdateIncomeInDto GetEntity(UpdateIncomeInDto entity)
    {
        return this;
    }
}