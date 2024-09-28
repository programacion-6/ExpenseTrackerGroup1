using BCrypt.Net;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;
using ExpenseTracker.Utils;
using ExpenseTracker.Utils.ParamValidator;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtGenerator _jwtTokenGenerator;
        private readonly string _secretKey;

        public AuthService(IUserRepository userRepository, IJwtGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _secretKey = Environment.GetEnvironmentVariable("JWT_SECRET") 
                         ?? throw new ArgumentNullException("JWT_SECRET not found");
        }

        public async Task<string> Register(string name, string email, string password)
        {
            if (!EmailValidator.IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long.");
            }

            var existingUser = await _userRepository.GetUserByEmail(email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                PasswordHash = hashedPassword
            };

            await _userRepository.CreateEntity(newUser);
            return _jwtTokenGenerator.GenerateToken(newUser);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials.");
            }

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
