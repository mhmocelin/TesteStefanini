using Register.Application.Dispatcher.Interfaces;

namespace Register.Application.Commands.Persons.V2;

public class DeletePersonV2Command : IRequest<bool>
{
    public Guid Id { get; }

    public DeletePersonV2Command(Guid id)
    {
        Id = id;
    }
}
