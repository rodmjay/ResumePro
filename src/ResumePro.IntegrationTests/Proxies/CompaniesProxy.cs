using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class CompaniesProxy : BaseProxy, ICompaniesController
{
    private const string RootUrl = "v1.0/people/{0}/companies";

    public CompaniesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<CompanyDetails>> GetCompanies(int personId)
    {
        return await DoGetAsync<List<CompanyDetails>>(string.Format(RootUrl, personId));
    }

    public async Task<CompanyDetails> GetCompany(int personId, int companyId)
    {
        return await DoGetAsync<CompanyDetails>($"{string.Format(RootUrl, personId)}/{companyId}");
    }

    public async Task<ActionResult<CompanyDetails>> CreateCompany(int personId, CompanyOptions options)
    {
        return await DoPostActionResultAsync<CompanyOptions, CompanyDetails>(string.Format(RootUrl, personId), options);
    }

    public async Task<ActionResult<CompanyDetails>> UpdateCompany(int personId, int companyId, CompanyOptions options)
    {
        return await DoPutActionResultAsync<CompanyOptions, CompanyDetails>($"{string.Format(RootUrl, personId)}/{companyId}", options);
    }

    public async Task<Result> DeleteCompany(int personId, int jobId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId)}/{jobId}");
    }
}
