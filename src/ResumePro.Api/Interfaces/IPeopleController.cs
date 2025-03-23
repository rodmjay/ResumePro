using ResumePro.Shared.Filters;

namespace ResumePro.Api.Interfaces;

public interface IPeopleController
{
    Task<PagedList<PersonaDto>> GetPeople(
        PersonaFilters filters, PagingQuery paging);

    Task<PersonaDetails> GetPerson(int personId);
    Task<ActionResult<PersonaDetails>> CreatePerson(PersonOptions options);
    Task<ActionResult<PersonaDetails>> UpdatePerson(int personId, PersonOptions options);
    Task<Result> DeletePerson(int personId);
}