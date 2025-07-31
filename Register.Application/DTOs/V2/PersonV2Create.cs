namespace Register.Application.DTOs.V2;

public class PersonV2Create : PersonBaseDto
{
    public string CPF { get; set; }
    public AddressBaseDto Address { get; set; }

    public PersonV2Create()
    {
        Address = new AddressBaseDto();
    }

    public PersonV2Create(string name, string cpf, DateTime birthDate, string? gender, string? email,
                          string? placeOfBirth, string? nationality, AddressBaseDto address)
    {
        Name = name;
        CPF = cpf;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
        PlaceOfBirth = placeOfBirth;
        Nationality = nationality;
        Address = address;
    }
}