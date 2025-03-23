using Bespoke.Services.Interfaces;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IPeopleService : IService<Persona>
{
    Task<PagedList<T>> GetPeople<T>(int organizationId, PersonaFilters filters, PagingQuery paging)
        where T : PersonaDto;

    Task<T> GetPerson<T>(int organizationId, int personId) where T : PersonaDto;
    Task<Result> DeletePerson(int organizationId, int personId);
    Task<OneOf<PersonaDetails, Result>> CreatePerson(int organizationId, PersonOptions options);
    Task<OneOf<PersonaDetails, Result>> UpdatePerson(int organizationId, int personId, PersonOptions options);
}