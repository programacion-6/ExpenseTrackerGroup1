using ExpenseTracker.Dtos.IncomeDtos;
using FluentValidation;

public class CreateIncomeDtoValidator : AbstractValidator<CreateIncomeDto>
{
    public CreateIncomeDtoValidator()
    {
        RuleFor(income => income.Amount)
            .GreaterThan(0).WithMessage("The amount must be greater than 0.");

        RuleFor(income => income.Source)
            .NotEmpty().WithMessage("The source cannot be empty.")
            .MaximumLength(100).WithMessage("The source must not exceed 100 characters.");

        RuleFor(income => income.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("The date cannot be in the future.");
    }
}

public class UpdateIncomeInDtoValidator : AbstractValidator<UpdateIncomeInDto>
{
    public UpdateIncomeInDtoValidator()
    {
        RuleFor(income => income.Amount)
            .GreaterThan(0).WithMessage("The amount must be greater than 0.");

        RuleFor(income => income.Source)
            .NotEmpty().WithMessage("The source cannot be empty.")
            .MaximumLength(100).WithMessage("The source must not exceed 100 characters.");

        RuleFor(income => income.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("The date cannot be in the future.");
    }
}
