using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Repository;
using ExpenseTracker.Dtos.ExpenseDtos;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto createExpenseDto, [FromQuery] Guid userId)
        {
            if (createExpenseDto == null)
                return BadRequest("Expense data is required.");

            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Amount = createExpenseDto.Amount, 
                Description = createExpenseDto.Description,
                Category = createExpenseDto.Category,
                Date = createExpenseDto.Date,
                CreatedAt = DateTime.UtcNow
            };

            var createdExpense = await _expenseRepository.CreateEntity(expense);
            return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, createdExpense);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var expenses = await _expenseRepository.GetAllEntities();
            return Ok(expenses); 
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            var expense = await _expenseRepository.ReadEntity(id);
            if (expense == null)
            {
                return NotFound("Expense not found");
            }

            return Ok(expense); 
        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] UpdateExpenseDto updateExpenseDto)
        {
            if (updateExpenseDto == null)
                return BadRequest("Expense data is required.");

            var updatedExpense = await _expenseRepository.UpdateEntity(id, updateExpenseDto.GetEntity(null));
            if (updatedExpense == null)
                return NotFound("Expense not found");

            return Ok(updatedExpense); 
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var deletedExpense = await _expenseRepository.DeleteEntity(id);
            if (deletedExpense == null)
            {
                return NotFound("Expense not found");
            }

            return NoContent(); 
        }
    }
}
