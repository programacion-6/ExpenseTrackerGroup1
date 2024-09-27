using ExpenseTracker.Dtos.IncomeDtos;

namespace ExpenseTracker.Interfaces.Service
{
    public interface IIncomeService
    {
        Task<IncomeDto> CreateIncomeAsync(CreateIncomeDto incomeDto);
        Task<IncomeDto?> GetIncomeByIdAsync(Guid id);
        Task<IEnumerable<IncomeDto>> GetIncomesByUserIdAsync(Guid userId);
        Task<bool> UpdateIncomeAsync(Guid id, UpdateIncomeInDto incomeDto);
        Task<bool> DeleteIncomeAsync(Guid id);
    }
}
