using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/schools/{schoolId}/degrees")]
public sealed class DegreesController : BaseController, IDegreesController
{
    private readonly IDegreeService _degreeService;

    public DegreesController(IServiceProvider serviceProvider, IDegreeService degreeService) : base(serviceProvider)
    {
        _degreeService = degreeService;
    }

    [HttpGet("{degreeId}")]
    public async Task<DegreeDto> GetDegree([FromRoute] int personId, [FromRoute] int schoolId, [FromRoute] int degreeId)
    {
        return await _degreeService.GetDegree<DegreeDto>(OrganizationId, personId, schoolId, degreeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<DegreeDto>> GetDegrees([FromRoute] int personId, [FromRoute] int schoolId)
    {
        return await _degreeService.GetDegrees<DegreeDto>(OrganizationId, personId, schoolId)
            .ConfigureAwait(false);
    }

    [HttpPut("{degreeId}")]
    public async Task<ActionResult<DegreeDto>> UpdateDegree([FromRoute] int personId, [FromRoute] int schoolId,
        [FromRoute] int degreeId, [FromBody] DegreeOptions options)
    {
        var result = await _degreeService.UpdateDegree(OrganizationId, personId, schoolId, degreeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{degreeId}")]
    public async Task<Result> DeleteDegree([FromRoute] int personId, [FromRoute] int schoolId,
        [FromRoute] int degreeId)
    {
        return await _degreeService.DeleteDegree(OrganizationId, personId, schoolId, degreeId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<DegreeDto>> CreateDegree([FromRoute] int personId, [FromRoute] int schoolId,
        [FromBody] DegreeOptions options)
    {
        var result = await _degreeService.CreateDegree(OrganizationId, personId, schoolId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}