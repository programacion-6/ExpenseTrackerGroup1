using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Dtos.GoalDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly GoalRepository _goalRepository;

    public GoalController(GoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto createGoalDto, [FromQuery] int userId)
    {
        if (createGoalDto == null)
            return BadRequest("Goal data is required.");

        var goal = new Goal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            GoalAmount = createGoalDto.GoalAmount,
            Deadline = createGoalDto.Deadline,
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
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalDto updateGoalDto)
    {
        if (updateGoalDto == null)
            return BadRequest("Goal data is required.");

        var updatedGoal = await _goalRepository.UpdateEntity(id, updateGoalDto);
        if (updatedGoal == null)
            return NotFound("Goal not found.");

        return Ok(updatedGoal);
    }
}