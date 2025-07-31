using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;

namespace Register.Application.Commands.Persons.V2;

public class CreatePersonV2Command : IRequest<PersonV2Response>
{
    public PersonV2Create Person { get; }

    public CreatePersonV2Command(PersonV2Create person)
    {
        Person = person;
    }
}
