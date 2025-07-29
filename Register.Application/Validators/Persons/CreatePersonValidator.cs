using FluentValidation;
using Register.Application.DTOs;
using System.Text.RegularExpressions;

namespace Register.Application.Validators.Persons;

public class CreatePersonValidator : AbstractValidator<PersonCreate>
{
    public CreatePersonValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF is required.")
            .Must(IsValidCpf).WithMessage("Invalid CPF format.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("BirthDate cannot be in the future.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("Invalid email format.");
    }

    private bool IsValidCpf(string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return false;

        // Remove caracteres não numéricos
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        // Rejeita sequências repetidas
        var invalidSequences = new[]
        {
        "00000000000",
        "11111111111",
        "22222222222",
        "33333333333",
        "44444444444",
        "55555555555",
        "66666666666",
        "77777777777",
        "88888888888",
        "99999999999"
    };

        if (invalidSequences.Contains(cpf))
            return false;

        // Cálculo dos dígitos verificadores
        int[] digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        // Primeiro dígito verificador
        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += digits[i] * (10 - i);

        int remainder = sum % 11;
        int firstVerifier = remainder < 2 ? 0 : 11 - remainder;

        if (digits[9] != firstVerifier)
            return false;

        // Segundo dígito verificador
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += digits[i] * (11 - i);

        remainder = sum % 11;
        int secondVerifier = remainder < 2 ? 0 : 11 - remainder;

        if (digits[10] != secondVerifier)
            return false;

        return true;
    }
}