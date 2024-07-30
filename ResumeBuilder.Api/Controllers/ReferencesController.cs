#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/references")]
public sealed class ReferencesController(IServiceProvider serviceProvider, IReferenceService referenceService)
    : BaseController(serviceProvider), IIndividualReferencesController
{
    [HttpGet("{referenceId}")]
    public async Task<ReferenceDto> Get([FromRoute] int referenceId)
    {
        return await referenceService.GetReference<ReferenceDto>(OrganizationId, UserId, referenceId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ReferenceDto>> GetReferences()
    {
        return await referenceService.GetReferences<ReferenceDto>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceDto>> CreateReference(
        [FromBody] ReferenceOptions options)
    {
        var result = await referenceService.CreateReference(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{referenceId}")]
    public async Task<ActionResult<ReferenceDto>> UpdateReference(
        [FromRoute] int referenceId,
        [FromBody] ReferenceOptions options)
    {
        var result = await referenceService.UpdateReference(OrganizationId, UserId, referenceId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{referenceId}")]
    public Task<Result> DeleteReference(
        [FromRoute] int referenceId)
    {
        return referenceService.DeleteReference(OrganizationId, UserId, referenceId);
    }
}