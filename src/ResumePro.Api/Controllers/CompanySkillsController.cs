using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/skills")]
public sealed class CompanySkillsController : BaseController, ICompanySkillsController
{
    private readonly ICompanySkillService _service;

    public CompanySkillsController(IServiceProvider serviceProvider, ICompanySkillService service) : base(
        serviceProvider)
    {
        _service = service;
    }

    [HttpPatch("{skillId}")]
    public async Task<Result> AddCompanySkill([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int skillId)
    {
        return await _service.AddCompanySkill(OrganizationId, personId, companyId, skillId)
            .ConfigureAwait(false);
    }

    [HttpDelete("{skillId}")]
    public async Task<Result> DeleteCompanySkill([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int skillId)
    {
        return await _service.DeleteCompanySkill(OrganizationId, personId, companyId, skillId)
            .ConfigureAwait(false);
    }
}