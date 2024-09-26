using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.BudgetDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Service
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IUserRepository _userRepository;

        public BudgetService(IBudgetRepository budgetRepository, IUserRepository userRepository)
        {
            _budgetRepository = budgetRepository;
            _userRepository = userRepository;
        }

        public async Task<Budget?> GetMonthlyBudget(Guid userId, DateTime month)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.");

            if (month > DateTime.Now)
                throw new ArgumentException("Cannot retrieve budget for a future month.");

            var user = await _userRepository.ReadEntity(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            return await _budgetRepository.GetMonthlyBudget(userId, month);
        }

        public async Task<Budget?> GetCurrentMonthBudget(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.");

            var user = await _userRepository.ReadEntity(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            return await _budgetRepository.GetCurrentMonthBudget(userId);
        }

        public async Task<Budget?> ReadEntity(Guid entityId)
        {
            if (entityId == Guid.Empty)
                throw new ArgumentException("Entity ID cannot be empty.");

            return await _budgetRepository.ReadEntity(entityId);
        }

        public async Task<Budget> CreateEntity(CreateBudgetDto budgetDto)
        {
            if (budgetDto.BudgetAmount <= 0)
                throw new ArgumentException("Budget amount must be greater than zero.");
            
            if (budgetDto.Month > DateTime.Now)
                throw new ArgumentException("Cannot create a budget for a future month.");

            var user = await _userRepository.ReadEntity(budgetDto.UserId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            return await _budgetRepository.CreateEntity(budgetDto.GetEntity(null));
        }

        public async Task<bool> UpdateEntity(Guid entityId, UpdateBudgetDto budgetDto)
        {
            if (entityId == Guid.Empty)
                throw new ArgumentException("Entity ID cannot be empty.");
            
            if (budgetDto.BudgetAmount <= 0)
                throw new ArgumentException("Budget amount must be greater than zero.");

            var budget = await _budgetRepository.ReadEntity(entityId);
            if (budget == null)
                throw new ArgumentException("Budget does not exist.");

            return await _budgetRepository.UpdateEntity(entityId, budgetDto.GetEntity(budget));
        }
    }
}
