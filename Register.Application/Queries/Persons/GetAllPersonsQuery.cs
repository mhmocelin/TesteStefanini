using Register.Application.Dispatcher;
using Register.Application.Dispatcher.Interfaces;
using Register.Application.DTOs;

namespace Register.Application.Queries.Persons;

public class GetAllPersonsQuery : IRequest<IEnumerable<PersonResponse>>                                  
{
}