using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Dtos.GoalDtos;

public class GoalDto : IOutDto<GoalDto, Goal>
{
    public Guid UserId { get; set; }
    public decimal GoalAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime Deadline { get; set; }
    

    public GoalDto GetDto(Goal entity)
    {
        return new GoalDto
        {
            UserId = entity.UserId,
            CurrentAmount = entity.CurrentAmount,
            GoalAmount = entity.GoalAmount,
            Deadline = entity.Deadline,
        };
    }
}