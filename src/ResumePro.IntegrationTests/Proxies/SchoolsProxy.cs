using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class SchoolsProxy : BaseProxy, ISchoolsController
{
    private const string RootUrl = "v1.0/people/{0}/schools";

    public SchoolsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<SchoolDetails>> GetSchools(int personId)
    {
        return await DoGetAsync<List<SchoolDetails>>(string.Format(RootUrl, personId));
    }

    public async Task<SchoolDetails> GetSchool(int personId, int schoolId)
    {
        return await DoGetAsync<SchoolDetails>($"{string.Format(RootUrl, personId)}/{schoolId}");
    }

    public async Task<ActionResult<SchoolDetails>> UpdateSchool(int personId, int schoolId, SchoolOptions options)
    {
        return await DoPutActionResultAsync<SchoolOptions, SchoolDetails>($"{string.Format(RootUrl, personId)}/{schoolId}", options);
    }

    public async Task<Result> DeleteSchool(int personId, int schoolId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId)}/{schoolId}");
    }

    public async Task<ActionResult<SchoolDetails>> CreateSchool(int personId, SchoolOptions options)
    {
        return await DoPostActionResultAsync<SchoolOptions, SchoolDetails>(string.Format(RootUrl, personId), options);
    }
}
