using FluentValidation;
using ExpenseTracker.Dtos.GoalDtos;

public class CreateGoalDtoValidator : AbstractValidator<CreateGoalDto>
{
    public CreateGoalDtoValidator()
    {
        RuleFor(x => x.GoalAmount)
            .GreaterThan(0).WithMessage("Goal amount must be greater than 0.");

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.Now).WithMessage("Deadline must be a future date.");
    }
}

public class UpdateGoalDtoValidator : AbstractValidator<UpdateGoalDto>
{
    public UpdateGoalDtoValidator()
    {
        RuleFor(x => x.GoalAmount)
            .GreaterThan(0).When(x => x.GoalAmount.HasValue).WithMessage("Goal amount must be greater than 0.");

        RuleFor(x => x.CurrentAmount)
            .GreaterThanOrEqualTo(0).When(x => x.CurrentAmount.HasValue).WithMessage("Current amount must be greater than or equal to 0.");

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.Now).When(x => x.Deadline.HasValue).WithMessage("Deadline must be a future date.");
    }
}