using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Dtos.IncomeDtos;
using ExpenseTracker.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] CreateIncomeDto incomeDto)
        {
            if (incomeDto == null)
                return BadRequest("Income data is required.");

            try
            {
                var userId = GetCurrentUserId();
                var createdIncome = await _incomeService.CreateIncomeAsync(userId, incomeDto);
                return CreatedAtAction(nameof(GetIncome), new { id = createdIncome.Id }, createdIncome);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIncomes()
        {
            try
            {
                var incomes = await _incomeService.GetAllIncomesAsync();
                return Ok(incomes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncome(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Income ID cannot be empty.");

            try
            {
                var income = await _incomeService.GetIncomeByIdAsync(id);
                if (income == null) return NotFound($"Income with ID {id} not found.");
                return Ok(income);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(Guid id, [FromBody] UpdateIncomeInDto updateIncomeDto)
        {
            if (id == Guid.Empty)
                return BadRequest("Income ID cannot be empty.");

            if (updateIncomeDto == null)
                return BadRequest("Income data is required.");

            try
            {
                var result = await _incomeService.UpdateIncomeAsync(id, updateIncomeDto);
                if (!result)
                    return NotFound($"Income with ID {id} not found or update failed.");

                return Ok("Income updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Income ID cannot be empty.");

            try
            {
                var deletedIncome = await _incomeService.DeleteIncomeAsync(id);
                return Ok("Income deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetIncomesByUserId()
        {
            try
            {
                var userId = GetCurrentUserId();
                var incomes = await _incomeService.GetIncomesByUserIdAsync(userId);
                return Ok(incomes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }
    }
}
