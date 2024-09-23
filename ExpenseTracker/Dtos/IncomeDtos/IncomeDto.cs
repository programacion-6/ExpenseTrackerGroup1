public class IncomeDto
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }
}
