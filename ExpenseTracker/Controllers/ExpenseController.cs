using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Repository;
using ExpenseTracker.Dtos.ExpenseDtos;
using ExpenseTracker.Domain;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseDto createdExpenseDto)
        {
            if (createdExpenseDto == null)
                return BadRequest("Expense data is required.");

            try
            {
                var expenseDto = await _expenseService.CreateExpenseAsync(createdExpenseDto);
                return CreatedAtAction(nameof(GetExpense), new { id = expenseDto.Id }, expenseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            try
            {
                var expenses = await _expenseService.GetAllExpensesAsync();
                return Ok(expenses);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Expense ID cannot be empty.");
            

           try
            {
                var expenseDto = await _expenseService.GetExpenseByIdAsync(id);
                if (expenseDto == null) return BadRequest("Expese not found");
                return Ok(expenseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] UpdateExpenseDto updateExpenseDto)
        {
            if (id == Guid.Empty)
                return BadRequest("Expense ID cannot be empty.");

            if (updateExpenseDto == null)
                return BadRequest("Expense data is required.");

            try
            {
                var result = await _expenseService.UpdateExpenseAsync(id, updateExpenseDto);
                if (!result)
                    return NotFound($"Expense with ID {id} not found or update failed.");

                return Ok("Expense updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Expense ID cannot be empty.");

            try
            {
                var deletedExpense = await _expenseService.DeleteExpenseAsync(id);
                return Ok("Expense deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
