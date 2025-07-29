using Register.Application.DTOs;
using static Register.Application.Dispatcher.IQuery;

namespace Register.Application.Queries.Persons;

public class GetAllPersonsQuery : IQuery<IEnumerable<PersonResponse>>
{
}