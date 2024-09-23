using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeRepository _incomeRepository;

    public IncomeController(IIncomeRepository incomeRepository)
    {
        _incomeRepository = incomeRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IncomeDto incomeDto)
    {
        if (incomeDto == null)
            return BadRequest("Invalid income data.");

        var income = new Income(Guid.NewGuid(), incomeDto.Amount, incomeDto.Source, incomeDto.Date);
        var createdIncome = await _incomeRepository.CreateEntity(income);
        
        return CreatedAtAction(nameof(Read), new { id = createdIncome.Id }, createdIncome);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Read(Guid id)
    {
        var income = await _incomeRepository.ReadEntity(id);
        if (income == null)
            return NotFound();

        return Ok(income);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIncomeDto updateIncomeDto)
    {
        if (updateIncomeDto == null)
            return BadRequest("Invalid income data.");

        var updatedIncome = await _incomeRepository.UpdateEntity(id, updateIncomeDto);
        if (updatedIncome == null)
            return NotFound();

        return Ok(updatedIncome);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedIncome = await _incomeRepository.DeleteEntity(id);
        if (deletedIncome == null)
            return NotFound();

        return NoContent();
    }
/**
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        var incomes = await _incomeRepository.GetAllEntities(query);
        return Ok(incomes);
    }**/
}
