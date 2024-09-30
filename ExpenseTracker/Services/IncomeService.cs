using ExpenseTracker.Dtos.IncomeDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;
using FluentValidation;

namespace ExpenseTracker.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateIncomeDto> _createIncomeValidator;
        private readonly IValidator<UpdateIncomeInDto> _updateIncomeValidator;

        public IncomeService(IIncomeRepository incomeRepository, IUserRepository userRepository, IValidator<CreateIncomeDto> createIncomeValidator, IValidator<UpdateIncomeInDto> updateIncomeValidator)
        {
            _incomeRepository = incomeRepository;
            _userRepository = userRepository;
            _createIncomeValidator = createIncomeValidator;
            _updateIncomeValidator = updateIncomeValidator;

        }

        public async Task<Income> CreateIncomeAsync(Guid userId, CreateIncomeDto incomeDto)
        {
            if (incomeDto == null)
                throw new ArgumentNullException(nameof(incomeDto));

            var validationResult = await _createIncomeValidator.ValidateAsync(incomeDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await _userRepository.ReadEntity(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            var income = incomeDto.GetEntity(new Income{UserId = user.Id});
            var createdIncome = await _incomeRepository.CreateEntity(income);

            return createdIncome;
        }
        public  async Task<List<Income>> GetAllIncomesAsync()
        {
            return await _incomeRepository.GetAllEntities(); 
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
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            var user = await _userRepository.ReadEntity(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.", nameof(userId));

            var incomes = await _incomeRepository.GetIncomesByUserId(userId);
            return incomes.Select(income => new IncomeDto().GetDto(income));
        }

        public async Task<bool> UpdateIncomeAsync(Guid id, UpdateIncomeInDto incomeDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Income ID cannot be empty.");

            if (incomeDto == null)
                throw new ArgumentNullException(nameof(incomeDto));
            
            var validationResult = await _updateIncomeValidator.ValidateAsync(incomeDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var income = await _incomeRepository.ReadEntity(id);
            if (income == null)
                throw new ArgumentException("Income does not exist.");

            var updatedIncome = incomeDto.GetEntity(income);
            return await _incomeRepository.UpdateEntity(id, updatedIncome);
        }

        public async Task<Income?> DeleteIncomeAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Income ID cannot be empty.");

            var income = await _incomeRepository.ReadEntity(id);
            if (income == null)
                throw new ArgumentException("Income does not exist.");

            return await _incomeRepository.DeleteEntity(id);
        }
    }
}
