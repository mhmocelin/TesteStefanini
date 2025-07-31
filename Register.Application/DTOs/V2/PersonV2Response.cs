using Register.Domain.Entities;

namespace Register.Application.DTOs.V2;

public class PersonV2Response : PersonBaseDto
{
    public Guid Id { get; set; }
    public string CPF { get; set; }
    public AddressBaseDto Address { get; set; }

    public PersonV2Response()
    {
        Address = new AddressBaseDto();
    }

    public PersonV2Response(Guid id, string name, string cpf, DateTime birthDate, string? gender, string? email,
                            string? placeOfBirth, string? nationality, AddressBaseDto address)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
        PlaceOfBirth = placeOfBirth;
        Nationality = nationality;
        Address = address;
    }

    public PersonV2Response(Person person)
    {

        Id = person.Id;
        Name = person.Name;
        CPF = person.CPF;
        BirthDate = person.BirthDate;
        Gender = person.Gender;
        Email = person.Email;
        PlaceOfBirth = person.PlaceOfBirth;
        Nationality = person.Nationality;
        Address = person.Address == null ? 
            new AddressBaseDto() : 
            new AddressBaseDto(
                person.Address.Street,
                person.Address.Number,
                person.Address.Neighborhood,
                person.Address.City,
                person.Address.State,
                person.Address.Country
            );
    }

    public static PersonV2Response FromEntity(Person person)
    {
        return new PersonV2Response(person);
    }
}
