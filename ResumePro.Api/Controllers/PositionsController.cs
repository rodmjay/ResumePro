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
}
