using static Register.Application.Dispatcher.ICommand;

namespace Register.Application.Commands.Persons;

public class DeletePersonCommand : ICommand<bool>
{
    public Guid Id { get; }

    public DeletePersonCommand(Guid id)
    {
        Id = id;
    }
}