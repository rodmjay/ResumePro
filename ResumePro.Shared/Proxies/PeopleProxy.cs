#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Helpers;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class PeopleProxy(HttpClient httpClient) : BaseProxy(httpClient), IPeopleController
{
    public async Task<PagedList<PersonaDto>> GetPeople(PersonaFilters filters, PagingQuery paging)
    {
        var querystring = UrlHelper.CombineObjectsToUrl(paging, filters);

        return await DoPost<PersonaFilters, PagedList<PersonaDto>>($"v1.0/people/search?{querystring}", filters)
            .ConfigureAwait(false);
    }

    public async Task<PersonaDetails> GetPerson(int personId)
    {
        return await DoGet<PersonaDetails>($"v1.0/people/{personId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<PersonaDetails>> CreatePerson(PersonOptions options)
    {
        return await DoPostActionResult<PersonOptions, PersonaDetails>("v1.0/people", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<PersonaDetails>> UpdatePerson(int personId, PersonOptions options)
    {
        return await DoPutActionResult<PersonOptions, PersonaDetails>($"v1.0/people/{personId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeletePerson(int personId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}")
            .ConfigureAwait(false);
    }
}