using FluentValidation;
using Register.Application.DTOs.V2;

namespace Register.Application.Validators.Persons.V2;

public class UpdatePersonV2Validator : AbstractValidator<PersonV2Update>
{
    public UpdatePersonV2Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("BirthDate cannot be in the future.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required.");

        RuleFor(x => x.Address.Street)
            .NotEmpty().WithMessage("Street is required.");

        RuleFor(x => x.Address.Number)
            .NotEmpty().WithMessage("Number is required.");

        RuleFor(x => x.Address.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Address.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Address.Country)
            .NotEmpty().WithMessage("Country is required.");
    }
}
