using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;

namespace Register.Application.Commands.Persons;

public class UpdatePersonCommand : IRequest<PersonResponse>
{
    public Guid Id { get; }
    public PersonUpdate Request { get; }

    public UpdatePersonCommand(Guid id, PersonUpdate request)
    {
        Id = id;
        Request = request;
    }
}