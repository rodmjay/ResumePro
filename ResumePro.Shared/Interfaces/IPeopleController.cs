#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Interfaces;

public interface IPeopleController
{
    Task<PagedList<PersonaDto>> GetPeople(
        PersonaFilters filters, PagingQuery paging);

    Task<PersonaDetails> GetPerson(int personId);
    Task<ActionResult<PersonaDetails>> CreatePerson(PersonaOptions options);
    Task<ActionResult<PersonaDetails>> UpdatePerson(int personId, PersonaOptions options);
    Task<Result> DeletePerson(int personId);
}