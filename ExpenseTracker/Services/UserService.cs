using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.UserDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;
using ExpenseTracker.Utils.ParamValidator;

namespace ExpenseTracker.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        var user = await _userRepository.ReadEntity(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
        return user;
    }

    public async Task<User?> UpdateUser(Guid userId, UpdateDto updateDto)
    {
        var existingUser = await _userRepository.ReadEntity(userId);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
        if (!EmailValidator.IsValidEmail(updateDto.GetEntity(existingUser).Email))
        {
            throw new ArgumentException("Invalid email format.");
        }
        var updatedUser = updateDto.GetEntity(existingUser); 
        var result = await _userRepository.UpdateEntity(userId,updatedUser);

        if (!result)
        {
            throw new Exception("Failed to update user");
        }
        return updatedUser;
    }

    public async Task<User?> DeleteUser(Guid userId)
    {
        var existingUser = await GetUserById(userId);

        if (existingUser == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        return await _userRepository.DeleteEntity(userId);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        if (!EmailValidator.IsValidEmail(email))
        {
            throw new ArgumentException("Invalid email format.");
        }

        var user = await _userRepository.GetUserByEmail(email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {email} not found.");
        }
        return user;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllEntities();
    }
}
