using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;

namespace Register.Application.Commands.Persons;

public class CreatePersonCommand : IRequest<PersonResponse>
{
    public PersonCreate Person { get; }

    public CreatePersonCommand(PersonCreate personDto)
    {
        Person = personDto;
    }
}