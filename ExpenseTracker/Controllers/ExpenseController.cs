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
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto createExpenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = createExpenseDto.GetDto();
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
                return NotFound();
            }

            return Ok(expense); 
        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] UpdateExpenseDto updateExpenseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedExpense = await _expenseRepository.UpdateEntity(id, updateExpenseDto);
            if (updatedExpense == null)
            {
                return NotFound();
            }

            return Ok(updatedExpense); 
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            var deletedExpense = await _expenseRepository.DeleteEntity(id);
            if (deletedExpense == null)
            {
                return NotFound();
            }

            return NoContent(); 
        }
    }
}
