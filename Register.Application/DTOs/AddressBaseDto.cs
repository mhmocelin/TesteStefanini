namespace Register.Application.DTOs;

public class AddressBaseDto
{
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public AddressBaseDto() { }

    public AddressBaseDto(string street, string number, string neighborhood, string city, string state, string country)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }
}