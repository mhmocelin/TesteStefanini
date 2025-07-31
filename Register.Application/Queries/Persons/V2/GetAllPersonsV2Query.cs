using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs.V2;

namespace Register.Application.Queries.Persons.V2;

public class GetAllPersonsV2Query : IRequest<IEnumerable<PersonV2Response>>
{
}
