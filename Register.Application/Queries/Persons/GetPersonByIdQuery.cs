using Register.Application.DTOs;
using Register.Application.Dispatcher;
using Register.Application.Dispatcher.Interfaces;

namespace Register.Application.Queries.Persons;

public class GetPersonByIdQuery : IRequest<PersonResponse?>
{
    public Guid Id { get; }

    public GetPersonByIdQuery(Guid id)
    {
        Id = id;
    }
}