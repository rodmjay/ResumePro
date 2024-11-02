#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class CompaniesProxy(HttpClient httpClient) : BaseProxy(httpClient), ICompaniesController
{
    public async Task<List<CompanyDetails>> GetCompanies(int personId)
    {
        return await DoGet<List<CompanyDetails>>($"v1.0/people/{personId}/jobs")
            .ConfigureAwait(false);
    }

    public async Task<CompanyDetails> GetCompany(int personId, int companyId)
    {
        return await DoGet<CompanyDetails>($"v1.0/people/{personId}/jobs/{companyId}")
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<CompanyDetails>> CreateCompany(int personId, CompanyOptions options)
    {
        return await DoPostActionResult<CompanyOptions, CompanyDetails>($"v1.0/people/{personId}/jobs", options)
            .ConfigureAwait(false);
    }

    public async Task<ActionResult<CompanyDetails>> UpdateCompany(int personId, int companyId, CompanyOptions options)
    {
        return await DoPutActionResult<CompanyOptions, CompanyDetails>($"v1.0/people/{personId}/jobs/{companyId}", options)
            .ConfigureAwait(false);
    }

    public async Task<Result> DeleteCompany(int personId, int jobId)
    {
        return await DoDelete<Result>($"v1.0/people/{personId}/jobs/{jobId}")
            .ConfigureAwait(false);
    }
}