using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Dtos.GoalDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalRepository _goalRepository;

    public GoalController(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalInDto createGoalInDto, [FromQuery] Guid userId)
    {
        if (createGoalInDto == null)
            return BadRequest("Goal data is required.");

        var goal = new Goal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GoalAmount = createGoalInDto.GoalAmount,
            Deadline = createGoalInDto.Deadline,
            CurrentAmount = 0
        };

        var createdGoal = await _goalRepository.CreateEntity(goal);
        return CreatedAtAction(nameof(GetGoal), new { id = createdGoal.Id }, createdGoal);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetGoals([FromQuery] Guid userId)
    {
        var goals = await _goalRepository.GetGoalsByUserId(userId);
        return Ok(goals);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGoal(Guid id)
    {
        var goal = await _goalRepository.ReadEntity(id);
        if (goal == null)
            return NotFound("Goal not found.");
        
        return Ok(goal);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalInDto updateGoalInDto)
    {
        if (updateGoalInDto == null)
            return BadRequest("Goal data is required.");

        var updatedGoal = await _goalRepository.UpdateEntity(id, updateGoalInDto.GetEntity(null));
        if (updatedGoal == null)
            return NotFound("Goal not found.");

        return Ok(updatedGoal);
    }
}