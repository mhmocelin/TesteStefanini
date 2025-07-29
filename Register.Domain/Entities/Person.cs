namespace Register.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PlaceOfBirth { get; set; }
    public string? Nationality { get; set; }
    public string CPF { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Person(string name, string cpf, DateTime birthDate,
                  string? gender = null, string? email = null,
                  string? placeOfBirth = null, string? nationality = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF is required.");

        if (birthDate > DateTime.UtcNow)
            throw new ArgumentException("Birth date cannot be in the future.");

        Id = Guid.NewGuid();
        Name = name;
        CPF = cpf;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
        PlaceOfBirth = placeOfBirth;
        Nationality = nationality;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, DateTime birthDate,
                       string? gender = null, string? email = null,
                       string? placeOfBirth = null, string? nationality = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.");

        if (birthDate > DateTime.UtcNow)
            throw new ArgumentException("Birth date cannot be in the future.");

        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
        PlaceOfBirth = placeOfBirth;
        Nationality = nationality;
        UpdatedAt = DateTime.UtcNow;
    }
}
