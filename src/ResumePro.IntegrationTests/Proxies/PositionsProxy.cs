using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class PositionsProxy : BaseProxy, IPositionsController
{
    private const string RootUrl = "v1.0/people/{0}/companies/{1}/positions";

    public PositionsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ActionResult<CompanyDetails>> CreatePosition(int personId, int companyId, PositionOptions options)
    {
        return await DoPostActionResultAsync<PositionOptions, CompanyDetails>(string.Format(RootUrl, personId, companyId), options);
    }

    public async Task<ActionResult<CompanyDetails>> UpdatePosition(int personId, int companyId, int positionId, PositionOptions options)
    {
        return await DoPutActionResultAsync<PositionOptions, CompanyDetails>($"{string.Format(RootUrl, personId, companyId)}/{positionId}", options);
    }

    public async Task<Result> DeletePosition(int personId, int companyId, int positionId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId, companyId)}/{positionId}");
    }

    public async Task<List<PositionDetails>> GetPositions(int personId, int companyId)
    {
        return await DoGetAsync<List<PositionDetails>>(string.Format(RootUrl, personId, companyId));
    }

    public async Task<PositionDetails> GetPosition(int personId, int companyId, int positionId)
    {
        return await DoGetAsync<PositionDetails>($"{string.Format(RootUrl, personId, companyId)}/{positionId}");
    }
}
