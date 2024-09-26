using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.IncomeDtos;

public class UpdateIncomeDto : IDto<UpdateIncomeDto>
{
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }

    public UpdateIncomeDto(decimal amount, string source, DateTime date)
    {
        Source = source;
        Amount = amount;
        Date = date;
    }
    
    public UpdateIncomeDto GetEntity(UpdateIncomeDto entity)
    {
        return this;
    }
}