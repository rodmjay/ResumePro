using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions")]
public sealed class PositionsController : BaseController, IPositionsController
{
    private readonly IPositionService _positionService;

    public PositionsController(IServiceProvider serviceProvider, IPositionService positionService) : base(
        serviceProvider)
    {
        _positionService = positionService;
    }

    [HttpPost]
    public async Task<ActionResult<CompanyDetails>> CreatePosition([FromRoute] int personId, [FromRoute] int companyId,
        [FromBody] PositionOptions options)
    {
        var result = await _positionService.CreatePosition(OrganizationId, personId, companyId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{positionId}")]
    public async Task<ActionResult<CompanyDetails>> UpdatePosition(int personId, int companyId, int positionId,
        PositionOptions options)
    {
        var result = await _positionService.UpdatePosition(OrganizationId, personId, companyId, positionId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{positionId}")]
    public async Task<Result> DeletePosition([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId)
    {
        return await _positionService.DeletePosition(OrganizationId, personId, companyId, positionId);
    }

    [HttpGet]
    public async Task<List<PositionDetails>> GetPositions([FromRoute] int personId, [FromRoute] int companyId)
    {
        return await _positionService.GetPositions<PositionDetails>(OrganizationId, personId, companyId);
    }

    [HttpGet("{positionId}")]
    public async Task<PositionDetails> GetPosition(int personId, int companyId, int positionId)
    {
        return await _positionService.GetPosition<PositionDetails>(OrganizationId, personId, companyId, positionId);
    }
}