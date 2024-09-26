using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.UserDtos;

public class CreateUserDto : IInDto<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public User GetEntity(User? entity)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Email = Email,
            PasswordHash = Password
        };
    }
}