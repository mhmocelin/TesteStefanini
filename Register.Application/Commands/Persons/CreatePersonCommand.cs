using Register.Application.DTOs;
using static Register.Application.Dispatcher.ICommand;

namespace Register.Application.Commands.Persons;

public class CreatePersonCommand : ICommand<PersonResponse>
{
    public PersonCreate Person { get; }

    public CreatePersonCommand(PersonCreate personDto)
    {
        Person = personDto;
    }
}