#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/schools")]
public sealed class SchoolsController(IServiceProvider serviceProvider, ISchoolService schoolService)
    : BaseController(serviceProvider), IIndividualSchoolsController
{
    [HttpGet]
    public async Task<List<SchoolDetails>> GetSchools()
    {
        return await schoolService.GetSchools<SchoolDetails>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

    [HttpGet("{schoolId}")]
    public async Task<SchoolDetails> GetSchool([FromRoute] int schoolId)
    {
        return await schoolService.GetSchool<SchoolDetails>(OrganizationId, UserId, schoolId)
            .ConfigureAwait(false);
    }

    [HttpPut("{schoolId}")]
    public async Task<ActionResult<SchoolDetails>> UpdateSchool(
        [FromRoute] int schoolId,
        [FromBody] SchoolOptions options)
    {
        var result = await schoolService.UpdateSchool(OrganizationId, UserId, schoolId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }


    [HttpDelete("{schoolId}")]
    public async Task<Result> DeleteSchool(
        [FromRoute] int schoolId)
    {
        return await schoolService.DeleteSchool(OrganizationId, UserId, schoolId)
            .ConfigureAwait(false);
    }


    [HttpPost]
    public async Task<ActionResult<SchoolDetails>> CreateSchool(
        [FromBody] SchoolOptions options)
    {
        var result = await schoolService.CreateSchool(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}