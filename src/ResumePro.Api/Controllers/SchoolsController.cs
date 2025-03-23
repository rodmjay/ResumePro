using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/schools")]
public sealed class SchoolsController : BaseController, ISchoolsController
{
    private readonly ISchoolService _schoolService;

    public SchoolsController(IServiceProvider serviceProvider, ISchoolService schoolService) : base(serviceProvider)
    {
        _schoolService = schoolService;
    }

    [HttpGet]
    public async Task<List<SchoolDetails>> GetSchools([FromRoute] int personId)
    {
        return await _schoolService.GetSchools<SchoolDetails>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpGet("{schoolId}")]
    public async Task<SchoolDetails> GetSchool([FromRoute] int personId, [FromRoute] int schoolId)
    {
        return await _schoolService.GetSchool<SchoolDetails>(OrganizationId, personId, schoolId)
            .ConfigureAwait(false);
    }

    [HttpPut("{schoolId}")]
    public async Task<ActionResult<SchoolDetails>> UpdateSchool([FromRoute] int personId,
        [FromRoute] int schoolId,
        [FromBody] SchoolOptions options)
    {
        var result = await _schoolService.UpdateSchool(OrganizationId, personId, schoolId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }


    [HttpDelete("{schoolId}")]
    public async Task<Result> DeleteSchool([FromRoute] int personId,
        [FromRoute] int schoolId)
    {
        return await _schoolService.DeleteSchool(OrganizationId, personId, schoolId)
            .ConfigureAwait(false);
    }


    [HttpPost]
    public async Task<ActionResult<SchoolDetails>> CreateSchool([FromRoute] int personId,
        [FromBody] SchoolOptions options)
    {
        var result = await _schoolService.CreateSchool(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}