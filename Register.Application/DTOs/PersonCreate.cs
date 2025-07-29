namespace Register.Application.DTOs;

public class PersonCreate: PersonBaseDto
{
    public string CPF { get; set; } = string.Empty;
}
