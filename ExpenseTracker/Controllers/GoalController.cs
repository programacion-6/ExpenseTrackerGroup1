using ExpenseTracker.Dtos.GoalDtos;
using ExpenseTracker.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGoalById(Guid id)
        {
            try
            {
                var goal = await _goalService.GetGoalByIdAsync(id);
                if (goal == null)
                    return NotFound("Goal not found.");

                return Ok(goal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGoalsByUserId(Guid userId)
        {
            try
            {
                var goals = await _goalService.GetGoalsByUserIdAsync(userId);
                return Ok(goals);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto createGoalDto)
        {
            if (createGoalDto == null)
                return BadRequest("Goal data cannot be null.");

            try
            {
                var createdGoal = await _goalService.CreateGoalAsync(createGoalDto);
                return CreatedAtAction(nameof(GetGoalById), new { id = createdGoal.UserId }, createdGoal);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] UpdateGoalDto updateGoalDto)
        {
            if (updateGoalDto == null)
                return BadRequest("Goal data cannot be null.");

            try
            {
                var updated = await _goalService.UpdateGoalAsync(id, updateGoalDto);
                if (!updated)
                    return NotFound("Goal not found or update failed.");

                return Ok("Goal updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
