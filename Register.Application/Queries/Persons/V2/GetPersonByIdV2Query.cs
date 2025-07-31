using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;

namespace Register.Application.Queries.Persons.V2;

public class GetPersonByIdV2Query : IRequest<PersonV2Response>
{
    public Guid Id { get; }

    public GetPersonByIdV2Query(Guid id)
    {
        Id = id;
    }
}
