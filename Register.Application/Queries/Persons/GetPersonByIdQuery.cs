using Register.Application.DTOs;
using static Register.Application.Dispatcher.IQuery;

namespace Register.Application.Queries.Persons;

public class GetPersonByIdQuery : IQuery<PersonResponse?>
{
    public Guid Id { get; }

    public GetPersonByIdQuery(Guid id)
    {
        Id = id;
    }
}