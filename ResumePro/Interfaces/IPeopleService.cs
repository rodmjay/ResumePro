﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface IPeopleService : IService<Persona>
{
    Task<PagedList<T>> GetPeople<T>(int organizationId, PersonaFilters filters, PagingQuery paging)
        where T : PersonaDto;

    Task<T> GetPerson<T>(int organizationId, int personId) where T : PersonaDto;
    Task<Result> DeletePerson(int organizationId, int personId);
    Task<OneOf<PersonaDetails, Result>> CreatePerson(int organizationId, PersonOptions options);
    Task<OneOf<PersonaDetails, Result>> UpdatePerson(int organizationId, int personId, PersonOptions options);
}