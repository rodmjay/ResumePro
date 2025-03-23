using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class ReferencesProxy : BaseProxy, IReferencesController
{
    private const string RootUrl = "v1.0/people/{0}/references";

    public ReferencesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ReferenceDto> Get(int personId, int referenceId)
    {
        return await DoGetAsync<ReferenceDto>($"{string.Format(RootUrl, personId)}/{referenceId}");
    }

    public async Task<List<ReferenceDto>> GetReferences(int personId)
    {
        return await DoGetAsync<List<ReferenceDto>>(string.Format(RootUrl, personId));
    }

    public async Task<ActionResult<ReferenceDto>> CreateReference(int personId, ReferenceOptions options)
    {
        return await DoPostActionResultAsync<ReferenceOptions, ReferenceDto>(string.Format(RootUrl, personId), options);
    }

    public async Task<ActionResult<ReferenceDto>> UpdateReference(int personId, int referenceId, ReferenceOptions options)
    {
        return await DoPutActionResultAsync<ReferenceOptions, ReferenceDto>($"{string.Format(RootUrl, personId)}/{referenceId}", options);
    }

    public async Task<Result> DeleteReference(int personId, int referenceId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId)}/{referenceId}");
    }
}
