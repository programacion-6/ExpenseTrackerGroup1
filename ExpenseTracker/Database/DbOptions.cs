namespace ExpenseTracker.Persistence;

public class DbOptions
{
    public const string ConnectionStrings = nameof(ConnectionStrings);
    public string? DefaultConnection { get; set; }
    public string? ScriptsPath { get; set; }
}