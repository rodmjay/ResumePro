#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/schools/{schoolId}/degrees")]
public sealed class DegreesController(IServiceProvider serviceProvider, IDegreeService degreeService)
    : BaseController(serviceProvider), IIndividualDegreesController
{
    [HttpGet("{degreeId}")]
    public async Task<DegreeDto> GetDegree([FromRoute] int schoolId, [FromRoute] int degreeId)
    {
        return await degreeService.GetDegree<DegreeDto>(OrganizationId, UserId, schoolId, degreeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<DegreeDto>> GetDegrees([FromRoute] int schoolId)
    {
        return await degreeService.GetDegrees<DegreeDto>(OrganizationId, UserId, schoolId)
            .ConfigureAwait(false);
    }

    [HttpPut("{degreeId}")]
    public async Task<ActionResult<DegreeDto>> UpdateDegree([FromRoute] int schoolId,
        [FromRoute] int degreeId, [FromBody] DegreeOptions options)
    {
        var result = await degreeService.UpdateDegree(OrganizationId, UserId, schoolId, degreeId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{degreeId}")]
    public async Task<Result> DeleteDegree([FromRoute] int schoolId,
        [FromRoute] int degreeId)
    {
        return await degreeService.DeleteDegree(OrganizationId, UserId, schoolId, degreeId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<DegreeDto>> CreateDegree([FromRoute] int schoolId,
        [FromBody] DegreeOptions options)
    {
        var result = await degreeService.CreateDegree(OrganizationId, UserId, schoolId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}