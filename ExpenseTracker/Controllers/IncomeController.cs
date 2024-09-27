using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Dtos.IncomeDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

[ApiController]
[Route("api/[controller]")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIncomeDto incomeDto)
    {
        if (incomeDto == null)
            return BadRequest("Income data is required.");

        try
        {
            var createdIncome = await _incomeService.CreateIncomeAsync(incomeDto);

            return CreatedAtAction(nameof(Read), new { id = createdIncome.UserId }, createdIncome);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Income ID cannot be empty.");

        try
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
                return NotFound($"Income with ID {id} not found.");

            return Ok(income);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIncomeInDto updateIncomeDto)
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
    public async Task<IActionResult> Delete(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Income ID cannot be empty.");

        try
        {
            var deletedIncome = await _incomeService.DeleteIncomeAsync(id);
            if (!deletedIncome)
                return NotFound($"Income with ID {id} not found.");

            return Ok("Income deleted successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
