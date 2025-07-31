using Register.Application.Dispatcher;
using Register.Application.Dispatcher.Interfaces;

namespace Register.Application.Commands.Persons;

public class DeletePersonCommand : IRequest<bool>
{
    public Guid Id { get; }

    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }
}