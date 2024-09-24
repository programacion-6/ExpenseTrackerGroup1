using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Domain;
using ExpenseTracker.Dtos.BudgetDtos;
using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetController : ControllerBase
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetController(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetDto createBudgetDto)
    {
        if (createBudgetDto == null)
            return BadRequest("Budget data is required.");

        var budget = new Budget(Guid.NewGuid(), createBudgetDto.UserId, createBudgetDto.BudgetAmount, createBudgetDto.Month);
        var createdBudget = await _budgetRepository.CreateEntity(budget);
        
        return CreatedAtAction(nameof(GetMonthlyBudget), new { userId = createdBudget.UserId, month = createdBudget.Month }, createdBudget);
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentMonthBudget(Guid userId)
    {
        try
        {
            var currentMonth = DateTime.UtcNow;
            var budget = await _budgetRepository.GetMonthlyBudget(userId, currentMonth);
            return Ok(budget);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{month}")]
    public async Task<IActionResult> GetMonthlyBudget(Guid userId, string month)
    {
        if (DateTime.TryParse($"{month}-01", out var parsedMonth))
        {
            try
            {
                var budget = await _budgetRepository.GetMonthlyBudget(userId, parsedMonth);
                return Ok(budget);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        else
        {
            return BadRequest("Invalid month format. Use 'yyyy-MM'.");
        }
    }

    [HttpGet("remaining")]
    public async Task<IActionResult> GetRemainingBudget(Guid userId)
    {
        try
        {
            var budget = await _budgetRepository.GetRemainingBudget(userId);
            return Ok(budget);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}