using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class PeopleProxy : BaseProxy, IPeopleController
{
    private const string RootUrl = "v1.0/people";

    public PeopleProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<PagedList<PersonaDto>> GetPeople(PersonaFilters filters, PagingQuery paging)
    {
        return await DoPostAsync<PersonaFilters, PagedList<PersonaDto>>($"{RootUrl}/Search", filters);
    }

    public async Task<PersonaDetails> GetPerson(int personId)
    {
        return await DoGetAsync<PersonaDetails>($"{RootUrl}/{personId}");
    }

    public async Task<ActionResult<PersonaDetails>> CreatePerson(PersonOptions options)
    {
        return await DoPostActionResultAsync<PersonOptions, PersonaDetails>(RootUrl, options);
    }

    public async Task<ActionResult<PersonaDetails>> UpdatePerson(int personId, PersonOptions options)
    {
        return await DoPutActionResultAsync<PersonOptions, PersonaDetails>($"{RootUrl}/{personId}", options);
    }

    public async Task<Result> DeletePerson(int personId)
    {
        return await DoDeleteAsync<Result>($"{RootUrl}/{personId}");
    }
}
