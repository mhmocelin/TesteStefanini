using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Register.Application.DTOs;
using Register.Application.Exceptions;
using Register.Application.Interfaces;
using Register.Domain.Entities;
using Register.Infrastructure.Data;

namespace Register.Application.Services;

public class PersonService : IPersonService
{
    private readonly AppDbContext _context;
    private readonly IValidator<PersonCreate> _createValidator;
    private readonly IValidator<PersonUpdate> _updateValidator;

    public PersonService(
        AppDbContext context,
        IValidator<PersonCreate> createValidator,
        IValidator<PersonUpdate> updateValidator)
    {
        _context = context;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<PersonResponse> CreateAsync(PersonCreate personDto)
    {
        var validation = await _createValidator.ValidateAsync(personDto);

        if (!validation.IsValid)
        {
            var error = validation.Errors.First();
            throw new BusinessException(error.ErrorMessage, error.ErrorCode);
        }
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

        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();

        return MapToResponse(person);
    }

    public async Task<PersonResponse?> UpdateAsync(Guid id, PersonUpdate personDto)
    {
        var validation = await _updateValidator.ValidateAsync(personDto);

        if (!validation.IsValid)
        {
            var error = validation.Errors.First();
            throw new BusinessException(error.ErrorMessage, error.ErrorCode);
        }

        var person = await _context.Persons.FindAsync(id);
        if (person == null)
            return null;

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
        => PersonResponse.FromEntity(person);
}
