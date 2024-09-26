using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.GoalDtos;

public class CreateGoalDto : IDto<CreateGoalDto>
{
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }

    public CreateGoalDto(decimal goalAmount, DateTime deadline)
    {
        GoalAmount = goalAmount;
        Deadline = deadline;
    }

    public CreateGoalDto GetEntity(CreateGoalDto entity)
    {
        return this;
    }
}