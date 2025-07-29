using Register.Application.DTOs;
using Register.Application.Interfaces;
using Register.Domain.Entities;
using Register.Infrastructure.Data;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace Register.Application.Services;

public class PersonService : IPersonService
{
    private readonly AppDbContext _context;

    public PersonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PersonResponse> CreateAsync(PersonCreate personDto)
    {
        ValidateCpf(personDto.CPF);
        ValidateEmail(personDto.Email);
        ValidateBirthDate(personDto.BirthDate);

        var exists = await _context.Persons.AnyAsync(p => p.CPF == personDto.CPF);
        if (exists)
            throw new InvalidOperationException("CPF already exists.");

        var person = new Person(
            personDto.Name,
            personDto.CPF,
            personDto.BirthDate,
            personDto.Gender,
            personDto.Email,
            personDto.PlaceOfBirth,
            personDto.Nationality
        );

        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        return MapToResponse(person);
    }

    public async Task<PersonResponse?> UpdateAsync(Guid id, PersonUpdate personDto)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null)
            return null;

        ValidateEmail(personDto.Email);
        ValidateBirthDate(personDto.BirthDate);

        person.Update(
            personDto.Name,
            personDto.BirthDate,
            personDto.Gender,
            personDto.Email,
            personDto.PlaceOfBirth,
            personDto.Nationality
        );

        await _context.SaveChangesAsync();

        return MapToResponse(person);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null)
            return false;

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PersonResponse?> GetByIdAsync(Guid id)
    {
        var person = await _context.Persons.FindAsync(id);
        return person == null ? null : MapToResponse(person);
    }

    public async Task<IEnumerable<PersonResponse>> GetAllAsync()
    {
        var persons = await _context.Persons.ToListAsync();
        return persons.Select(MapToResponse);
    }

    private static PersonResponse MapToResponse(Person person)
    {
        return PersonResponse.FromEntity(person);
    }

    private static void ValidateCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || !Regex.IsMatch(cpf, @"^\d{11}$"))
            throw new ArgumentException("Invalid CPF format.");
    }

    private static void ValidateEmail(string? email)
    {
        if (!string.IsNullOrEmpty(email) &&
            !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.");
    }

    private static void ValidateBirthDate(DateTime birthDate)
    {
        if (birthDate > DateTime.UtcNow)
            throw new ArgumentException("Birth date cannot be in the future.");
    }
}
