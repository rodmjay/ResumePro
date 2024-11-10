#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions")]
public sealed class PositionsController(IServiceProvider serviceProvider, IPositionService positionService)
    : BaseController(serviceProvider), IPositionsController
{

    [HttpPost]
    public async Task<ActionResult<CompanyDetails>> CreatePosition([FromRoute] int personId, [FromRoute] int companyId,
        [FromBody] PositionOptions options)
    {
        var result = await positionService.CreatePosition(OrganizationId, personId, companyId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{positionId}")]
    public async Task<ActionResult<CompanyDetails>> UpdatePosition(int personId, int companyId, int positionId, PositionOptions options)
    {
        var result = await positionService.UpdatePosition(OrganizationId, personId, companyId, positionId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{positionId}")]
    public async Task<Result> DeletePosition([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId)
    {
        return await positionService.DeletePosition(OrganizationId, personId, companyId, positionId);
    }

    [HttpGet]
    public async Task<List<PositionDetails>> GetPositions([FromRoute] int personId, [FromRoute] int companyId)
    {
        return await positionService.GetPositions<PositionDetails>(OrganizationId, personId, companyId);
    }

    [HttpGet("{positionId}")]
    public async Task<PositionDetails> GetPosition(int personId, int companyId, int positionId)
    {
        return await positionService.GetPosition<PositionDetails>(OrganizationId, personId, companyId, positionId);
    }
}
