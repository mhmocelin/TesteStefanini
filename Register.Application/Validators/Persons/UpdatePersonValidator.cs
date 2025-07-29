using FluentValidation;
using Register.Application.DTOs;

namespace Register.Application.Validators.Persons;

public class UpdatePersonValidator : AbstractValidator<PersonUpdate>
{
    public UpdatePersonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("BirthDate cannot be in the future.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format.");
    }
}
