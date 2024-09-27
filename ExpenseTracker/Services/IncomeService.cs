using ExpenseTracker.Dtos.IncomeDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IUserRepository _userRepository;

        public IncomeService(IIncomeRepository incomeRepository, IUserRepository userRepository)
        {
            _incomeRepository = incomeRepository;
            _userRepository = userRepository;
        }

        public async Task<IncomeDto> CreateIncomeAsync(CreateIncomeDto incomeDto)
        {
            if (incomeDto == null)
                throw new ArgumentNullException(nameof(incomeDto));

            var user = await _userRepository.ReadEntity(incomeDto.UserId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            var income = incomeDto.GetEntity(null);
            var createdIncome = await _incomeRepository.CreateEntity(income);

            return new IncomeDto().GetDto(createdIncome);
        }

        public async Task<IncomeDto?> GetIncomeByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Income ID cannot be empty.");

            var income = await _incomeRepository.ReadEntity(id);
            if (income == null)
                return null;

            return new IncomeDto().GetDto(income);
        }

        public async Task<IEnumerable<IncomeDto>> GetIncomesByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.");

            var user = await _userRepository.ReadEntity(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            var incomes = await _incomeRepository.GetIncomesByUserId(userId);
            return incomes.Select(income => new IncomeDto().GetDto(income));
        }

        public async Task<bool> UpdateIncomeAsync(Guid id, UpdateIncomeInDto incomeDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Income ID cannot be empty.");

            if (incomeDto == null)
                throw new ArgumentNullException(nameof(incomeDto));

            var income = await _incomeRepository.ReadEntity(id);
            if (income == null)
                throw new ArgumentException("Income does not exist.");

            var updatedIncome = incomeDto.GetEntity(income);
            return await _incomeRepository.UpdateEntity(id, updatedIncome);
        }

        public async Task<bool> DeleteIncomeAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Income ID cannot be empty.");

            var income = await _incomeRepository.ReadEntity(id);
            if (income == null)
                throw new ArgumentException("Income does not exist.");

            var deletedIncome = await _incomeRepository.DeleteEntity(id);
            return deletedIncome != null;
        }
    }
}
