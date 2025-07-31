using FluentValidation;
using Register.Application.DTOs.V2;

namespace Register.Application.Validators.Persons.V2;

public class CreatePersonV2Validator : AbstractValidator<PersonV2Create>
{
    public CreatePersonV2Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF is required.")
            .Length(11).WithMessage("CPF must have 11 digits.")
            .Matches("^[0-9]*$").WithMessage("CPF must contain only numbers.");

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
