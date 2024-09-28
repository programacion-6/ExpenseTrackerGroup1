using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.IncomeDtos;

namespace ExpenseTracker.Interfaces.Service
{
    public interface IIncomeService
    {
        Task<IncomeDto?> GetIncomeByIdAsync(Guid id);
        Task<List<Income>> GetAllIncomesAsync();
        Task<Income> CreateIncomeAsync(Guid userId, CreateIncomeDto incomeDto);
        Task<bool> UpdateIncomeAsync(Guid id, UpdateIncomeInDto incomeDto);
        Task<Income?> DeleteIncomeAsync(Guid id);
        Task<IEnumerable<IncomeDto>> GetIncomesByUserIdAsync(Guid userId);
    }
}
