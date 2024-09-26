using ExpenseTracker.Interfaces;

namespace ExpenseTracker.Dtos.GoalDtos;

public class CreateGoalInDto : IInDto<CreateGoalInDto>
{
    public decimal GoalAmount { get; set; }
    public DateTime Deadline { get; set; }

    public CreateGoalInDto(decimal goalAmount, DateTime deadline)
    {
        GoalAmount = goalAmount;
        Deadline = deadline;
    }

    public CreateGoalInDto GetEntity(CreateGoalInDto entity)
    {
        return this;
    }
}