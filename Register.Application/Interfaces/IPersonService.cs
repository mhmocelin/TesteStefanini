using Register.Application.DTOs;

namespace Register.Application.Interfaces;

public interface IPersonService
{
    Task<PersonResponse> CreateAsync(PersonCreate person);
    Task<PersonResponse?> UpdateAsync(Guid id, PersonUpdate person);
    Task<bool> DeleteAsync(Guid id);
    Task<PersonResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<PersonResponse>> GetAllAsync();
}
