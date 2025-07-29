namespace Register.Application.DTOs;

public class PersonResponse : PersonBaseDto
{
    public Guid Id { get; set; }
    public string CPF { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public PersonResponse(Guid id, string name, string cpf, DateTime birthDate,
                         DateTime createdAt, DateTime? updatedAt = null,
                         string? gender = null, string? email = null,
                         string? placeOfBirth = null, string? nationality = null)
    {
        Id = id;
        Name = name;
        CPF = cpf;
        BirthDate = birthDate;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Gender = gender;
        Email = email;
        PlaceOfBirth = placeOfBirth;
        Nationality = nationality;
    }

    public static PersonResponse FromEntity(Domain.Entities.Person person)
    {
        return new PersonResponse(
            person.Id,
            person.Name,
            person.CPF,
            person.BirthDate,
            person.CreatedAt,
            person.UpdatedAt,
            person.Gender,
            person.Email,
            person.PlaceOfBirth,
            person.Nationality
        );
    }
}
