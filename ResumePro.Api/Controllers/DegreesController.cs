#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/schools/{schoolId}/degrees")]
public sealed class DegreesController(IServiceProvider serviceProvider, IDegreeService degreeService)
    : BaseController(serviceProvider), IDegreesController
{
    [HttpGet("{degreeId}")]
    public async Task<DegreeDto> GetDegree([FromRoute] int personId, [FromRoute] int schoolId, [FromRoute] int degreeId)
    {
        return await degreeService.GetDegree<DegreeDto>(OrganizationId, personId, schoolId, degreeId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<DegreeDto>> GetDegrees([FromRoute] int personId, [FromRoute] int schoolId)
    {
        return await degreeService.GetDegrees<DegreeDto>(OrganizationId, personId, schoolId)
            .ConfigureAwait(false);
    }

    [HttpPut("{degreeId}")]
    public async Task<ActionResult<DegreeDto>> UpdateDegree([FromRoute] int personId, [FromRoute] int schoolId, 
        [FromRoute]int degreeId, [FromBody]DegreeOptions options)
    {
        var result = await degreeService.UpdateDegree(OrganizationId, personId, schoolId, degreeId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{degreeId}")]
    public async Task<Result> DeleteDegree([FromRoute] int personId, [FromRoute] int schoolId,
        [FromRoute] int degreeId)
    {
        return await degreeService.DeleteDegree(OrganizationId, personId, schoolId, degreeId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<DegreeDto>> CreateDegree([FromRoute] int personId, [FromRoute] int schoolId, [FromBody] DegreeOptions options)
    {
        var result = await degreeService.CreateDegree(OrganizationId, personId, schoolId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }
}