using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies")]
public sealed class CompaniesController : BaseController, ICompaniesController
{
    private readonly ICompanyService _companyService;

    public CompaniesController(IServiceProvider serviceProvider, ICompanyService companyService) : base(serviceProvider)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<List<CompanyDetails>> GetCompanies([FromRoute] int personId)
    {
        return await _companyService.GetCompanies<CompanyDetails>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpGet("{companyId}")]
    public async Task<CompanyDetails> GetCompany([FromRoute] int personId, [FromRoute] int companyId)
    {
        return await _companyService.GetCompany<CompanyDetails>(OrganizationId, personId, companyId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDetails>> CreateCompany([FromRoute] int personId,
        [FromBody] CompanyOptions options)
    {
        var result = await _companyService.CreateCompany(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{companyId}")]
    public async Task<ActionResult<CompanyDetails>> UpdateCompany([FromRoute] int personId, [FromRoute] int companyId,
        [FromBody] CompanyOptions options)
    {
        var result = await _companyService.UpdateCompany(OrganizationId, personId, companyId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{jobId}")]
    public async Task<Result> DeleteCompany([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await _companyService.DeleteCompany(OrganizationId, personId, jobId)
            .ConfigureAwait(false);
    }
}