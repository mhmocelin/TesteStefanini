using Register.Application.DTOs;
using static Register.Application.Dispatcher.ICommand;

namespace Register.Application.Commands.Persons;

public class UpdatePersonCommand : ICommand<PersonResponse>
{
    public Guid Id { get; }
    public PersonUpdate Request { get; }

    public UpdatePersonCommand(Guid id, PersonUpdate request)
    {
        Id = id;
        Request = request;
    }
}