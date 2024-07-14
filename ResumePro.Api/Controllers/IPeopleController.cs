#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResumePro.Shared.Filters;

namespace ResumePro.Api.Controllers;

public interface IPeopleController
{
    Task<PagedList<PersonaDto>> GetPeople(
        PersonaFilters filters, PagingQuery paging);

    Task<PersonaDetails> GetPerson(int personId);
    Task<ActionResult<PersonaDetails>> CreatePerson( PersonaOptions options);
    Task<ActionResult<PersonaDetails>> UpdatePerson(int personId,  PersonaOptions options);
    Task<Result> DeletePerson(int personId);
}