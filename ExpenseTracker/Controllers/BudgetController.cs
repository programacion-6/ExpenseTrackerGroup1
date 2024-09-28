using System.Security.Claims;
using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.BudgetDtos;
using ExpenseTracker.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            try
            {
                var budget = await _budgetService.ReadEntity(id);
                if (budget == null)
                {
                    return NotFound("Budget not found.");
                }

                var budgetDto = new BudgetDto().GetDto(budget);
                return Ok(budgetDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyBudget([FromQuery] DateTime month)
        {
            try
            {
                var userId = GetCurrentUserId();
                var budget = await _budgetService.GetMonthlyBudget(userId, new DateTime(month.Year, month.Month, 1));
                if (budget == null)
                {
                    return NotFound("Budget not found for the specified month.");
                }

                var budgetDto = new BudgetDto().GetDto(budget);
                return Ok(budget);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentMonthBudget()
        {
            try
            {
                var userId = GetCurrentUserId();
                var budget = await _budgetService.GetCurrentMonthBudget(userId);
                if (budget == null)
                {
                    return NotFound("Budget not found for the current month.");
                }

                var budgetDto = new BudgetDto().GetDto(budget);
                return Ok(budget);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto createBudgetDto)
        {
            try
            {
                var userId = GetCurrentUserId();
                var budget = await _budgetService.CreateEntity(userId, createBudgetDto);
                var budgetDto = new BudgetDto().GetDto(budget);
                return CreatedAtAction(nameof(GetBudgetById),new { id = budgetDto.UserId }, budgetDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] UpdateBudgetDto updateBudgetDto)
        {
            if (updateBudgetDto == null)
            {
                return BadRequest("Budget data cannot be null.");
            }

            try
            {
                var updated = await _budgetService.UpdateEntity(id, updateBudgetDto);
                if (updated)
                {
                    return Ok("Budget updated successfully.");
                }
                else
                {
                    return NotFound("Budget not found.");
                }
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
