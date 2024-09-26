using ExpenseTracker.Domain;

namespace ExpenseTracker.Interfaces;

public interface IJwtGenerator
{
    string GenerateToken(User user);
}