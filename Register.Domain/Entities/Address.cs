namespace Register.Domain.Entities;

public class Address
{
    public Guid Id { get; private set; }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public Guid PersonId { get; private set; }
    public Person Person { get; private set; }
   protected Address() { }

    public Address(string street, string number, string neighborhood, string city, string state, string country)
    {
        Id = Guid.NewGuid();
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }

    public void Update(string street, string number, string? neighborhood,
                      string city, string state, string country)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
    }
}

