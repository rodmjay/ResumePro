using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class DegreesProxy : BaseProxy, IDegreesController
{
    private const string RootUrl = "v1.0/people/{0}/schools/{1}/degrees";

    public DegreesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<DegreeDto> GetDegree(int personId, int schoolId, int degreeId)
    {
        return await DoGetAsync<DegreeDto>($"{string.Format(RootUrl, personId, schoolId)}/{degreeId}");
    }

    public async Task<List<DegreeDto>> GetDegrees(int personId, int schoolId)
    {
        return await DoGetAsync<List<DegreeDto>>(string.Format(RootUrl, personId, schoolId));
    }

    public async Task<ActionResult<DegreeDto>> CreateDegree(int personId, int schoolId, DegreeOptions options)
    {
        return await DoPostActionResultAsync<DegreeOptions, DegreeDto>(string.Format(RootUrl, personId, schoolId), options);
    }

    public async Task<ActionResult<DegreeDto>> UpdateDegree(int personId, int schoolId, int degreeId, DegreeOptions options)
    {
        return await DoPutActionResultAsync<DegreeOptions, DegreeDto>($"{string.Format(RootUrl, personId, schoolId)}/{degreeId}", options);
    }

    public async Task<Result> DeleteDegree(int personId, int schoolId, int degreeId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId, schoolId)}/{degreeId}");
    }
}
