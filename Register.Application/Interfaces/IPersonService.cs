using Register.Application.DTOs;
using Register.Application.DTOs.V2;

namespace Register.Application.Interfaces;

public interface IPersonService
{
    Task<PersonResponse> CreateAsync(PersonCreate person);
    Task<PersonResponse?> UpdateAsync(Guid id, PersonUpdate person);
    Task<bool> DeleteAsync(Guid id);
    Task<PersonResponse?> GetByIdAsync(Guid id);
    Task<IEnumerable<PersonResponse>> GetAllAsync();

    Task<PersonV2Response?> GetByIdV2Async(Guid id);
    Task<IEnumerable<PersonV2Response>> GetAllV2Async();
    Task<PersonV2Response> CreateV2Async(PersonV2Create personDto);
    Task<PersonV2Response?> UpdateV2Async(Guid id, PersonV2Update personDto);
}
