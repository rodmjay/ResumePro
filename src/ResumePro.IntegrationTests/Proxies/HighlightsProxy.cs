using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class HighlightsProxy : BaseProxy, IHighlightsController
{
    private const string RootUrl = "v1.0/people/{0}/companies/{1}/positions/{2}/highlights";

    public HighlightsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<HighlightDto> GetHighlight(int personId, int companyId, int positionId, int highlightId)
    {
        return await DoGetAsync<HighlightDto>($"{string.Format(RootUrl, personId, companyId, positionId)}/{highlightId}");
    }

    public async Task<List<HighlightDto>> GetHighlights(int personId, int companyId, int positionId)
    {
        return await DoGetAsync<List<HighlightDto>>(string.Format(RootUrl, personId, companyId, positionId));
    }

    public async Task<ActionResult<HighlightDto>> CreateHighlight(int personId, int companyId, int positionId,
        HighlightOptions options)
    {
        return await DoPostActionResultAsync<HighlightOptions, HighlightDto>(string.Format(RootUrl, personId, companyId, positionId), options);
    }

    public async Task<ActionResult<HighlightDto>> UpdateHighlight(int personId, int companyId, int positionId,
        int highlightId, HighlightOptions options)
    {
        return await DoPutActionResultAsync<HighlightOptions, HighlightDto>($"{string.Format(RootUrl, personId, companyId, positionId)}/{highlightId}", options);
    }

    public async Task<Result> DeleteHighlight(int personId, int companyId, int positionId, int highlightId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId, companyId, positionId)}/{highlightId}");
    }
}
