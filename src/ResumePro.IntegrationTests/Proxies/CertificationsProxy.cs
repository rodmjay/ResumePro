using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class CertificationsProxy : BaseProxy, ICertificationsController
{
    private const string RootUrl = "v1.0/people/{0}/certifications";

    public CertificationsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<CertificationDto> Get(int personId, int certificationId)
    {
        return await DoGetAsync<CertificationDto>($"{string.Format(RootUrl, personId)}/{certificationId}");
    }

    public async Task<List<CertificationDto>> Get(int personId)
    {
        return await DoGetAsync<List<CertificationDto>>(string.Format(RootUrl, personId));
    }

    public async Task<ActionResult<CertificationDto>> CreateCertification(int personId, CertificationOptions options)
    {
        return await DoPostActionResultAsync<CertificationOptions, CertificationDto>(string.Format(RootUrl, personId), options);
    }

    public async Task<ActionResult<CertificationDto>> Update(int personId, int certificationId, CertificationOptions options)
    {
        return await DoPutActionResultAsync<CertificationOptions, CertificationDto>($"{string.Format(RootUrl, personId)}/{certificationId}", options);
    }

    public async Task<Result> Delete(int personId, int certificationId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId)}/{certificationId}");
    }
}
