using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.UserDtos;

public class UserUpdateDto : IDto<User>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    
    public User GetEntity(User? entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new User
        {
            Id = entity.Id,
            Name = Name ?? entity.Name,
            Email = Email ?? entity.Email,
            PasswordHash = entity.PasswordHash
        };
    }
}