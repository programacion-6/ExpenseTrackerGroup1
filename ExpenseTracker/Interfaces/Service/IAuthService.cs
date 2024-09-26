namespace ExpenseTracker.Interfaces.Service
{
    public interface IAuthService
    {
        Task<string> Register(string name, string email, string password);
        Task<string> Login(string email, string password);
    }
}