using System.Security.Claims;
using ExpenseTracker.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MetricController : ControllerBase
    {
        private readonly IMetricService _metricService;

        public MetricController(IMetricService metricService)
        {
            _metricService = metricService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] DateTime? month)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == Guid.Empty)
                {
                    return Unauthorized("User ID not found.");
                }
                var selectedMonth = month ?? DateTime.Now;
                var monthlySummary = await _metricService.GetMonthlySummary(userId, selectedMonth);
                return Ok(monthlySummary);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        
        [HttpGet("goals")]
        public async Task<IActionResult> GetUserGoalsWithProgress()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == Guid.Empty)
                {
                    return Unauthorized("User ID not found.");
                }
                var goalsProgress = await _metricService.GetUserGoalsWithProgress(userId);
                return Ok(goalsProgress);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        
        [HttpGet("expenseInsight")]
        public async Task<IActionResult> GetUserExpenseInsight()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == Guid.Empty)
                {
                    return Unauthorized("User ID not found.");
                }
                var goalsProgress = await _metricService.GetExpenseInsights(userId);
                return Ok(goalsProgress);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }
    }
}