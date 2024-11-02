#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies")]
public sealed class CompaniesController(IServiceProvider serviceProvider, ICompanyService companyService)
    : BaseController(serviceProvider), ICompaniesController
{
    [HttpGet]
    public async Task<List<CompanyDetails>> GetCompanies([FromRoute] int personId)
    {
        return await companyService.GetCompanies<CompanyDetails>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpGet("{companyId}")]
    public async Task<CompanyDetails> GetCompany([FromRoute] int personId, [FromRoute] int companyId)
    {
        return await companyService.GetCompany<CompanyDetails>(OrganizationId, personId, companyId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDetails>> CreateCompany([FromRoute] int personId, [FromBody] CompanyOptions options)
    {
        var result = await companyService.CreateCompany(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{companyId}")]
    public async Task<ActionResult<CompanyDetails>> UpdateCompany([FromRoute] int personId, [FromRoute] int companyId,
        [FromBody] CompanyOptions options)
    {
        var result = await companyService.UpdateCompany(OrganizationId, personId, companyId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{jobId}")]
    public async Task<Result> DeleteCompany([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await companyService.DeleteCompany(OrganizationId, personId, jobId)
            .ConfigureAwait(false);
    }
}