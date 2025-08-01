using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Register.Application.DTOs;
using Register.Application.DTOs.V2;
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
    private readonly IValidator<PersonV2Create> _createV2Validator;
    private readonly IValidator<PersonV2Update> _updateV2Validator;

    public PersonService(
       AppDbContext context,
       IValidator<PersonCreate> createValidator,
       IValidator<PersonUpdate> updateValidator,
       IValidator<PersonV2Create> createV2Validator,
       IValidator<PersonV2Update> updateV2Validator)
    {
        _context = context;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _createV2Validator = createV2Validator;
        _updateV2Validator = updateV2Validator;
    }

    #region V1 Methods
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

    #endregion

    #region V2 Methods
    private static PersonV2Response MapV2ToResponse(Person person)
        => PersonV2Response.FromEntity(person);

    public async Task<IEnumerable<PersonV2Response>> GetAllV2Async()
    {
        var persons = await _context.Persons.Include(p => p.Address).ToListAsync();
        return persons.Select(MapV2ToResponse);
    }

    public async Task<PersonV2Response?> GetByIdV2Async(Guid id)
    {
        var person = await _context.Persons.Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Id == id);

        return person == null ? null : new PersonV2Response(person);
    }

    public async Task<PersonV2Response> CreateV2Async(PersonV2Create personDto)
    {
        var validation = await _createV2Validator.ValidateAsync(personDto);

        if (!validation.IsValid)
        {
            var error = validation.Errors.First();
            throw new BusinessException(error.ErrorMessage, error.ErrorCode);
        }

        var exists = await _context.Persons.AnyAsync(p => p.CPF == personDto.CPF);
        if (exists)
            throw new InvalidOperationException("CPF already exists.");

        if (personDto.Address == null)
            throw new ArgumentException("Address is required");

        var person = new Person(
            personDto.Name,
            personDto.CPF,
            personDto.BirthDate,
            personDto.Gender,
            personDto.Email,
            personDto.PlaceOfBirth,
            personDto.Nationality
        );

        var address = new Address(
            personDto.Address.Street,
            personDto.Address.Number,
            personDto.Address.Neighborhood,
            personDto.Address.City,
            personDto.Address.State,
            personDto.Address.Country
        );

        person.Address = address;

        await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
        
        return new PersonV2Response(person);
    }

    public async Task<PersonV2Response?> UpdateV2Async(Guid id, PersonV2Update personDto)
    {
        var validation = await _updateV2Validator.ValidateAsync(personDto);

        if (!validation.IsValid)
        {
            var error = validation.Errors.First();
            throw new BusinessException(error.ErrorMessage, error.ErrorCode);
        }

        var person = await _context.Persons
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Id == id);

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

        if (person.Address == null)
        {
            person.Address = new Address(
                personDto.Address.Street,
                personDto.Address.Number,
                personDto.Address.Neighborhood,
                personDto.Address.City,
                personDto.Address.State,
                personDto.Address.Country
            );
            await _context.Addresses.AddAsync(person.Address);
        }
        else
        {
            person.Address.Update(
                personDto.Address.Street,
                personDto.Address.Number,
                personDto.Address.Neighborhood,
                personDto.Address.City,
                personDto.Address.State,
                personDto.Address.Country
            );
        }
        await _context.SaveChangesAsync();
        return PersonV2Response.FromEntity(person);
    }
    #endregion

}
