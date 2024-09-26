using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.UserDtos;

namespace ExpenseTracker.Interfaces.Service;

public interface IUserService
{
    Task<User?> GetUserById(Guid userId);
    Task<List<User>> GetAllUsers();
    Task<User?> UpdateUser(Guid userId, UserUpdateDto userUpdateDto);
    Task<User?> DeleteUser(Guid userId);
    Task<User?> GetUserByEmail(string email);
}