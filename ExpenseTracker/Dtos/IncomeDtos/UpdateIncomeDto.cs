public class UpdateIncomeDto : IDto<Income>
{
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }
}
