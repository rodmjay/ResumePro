#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/references")]
public sealed class ReferencesController(IServiceProvider serviceProvider, IReferenceService referenceService)
    : BaseController(serviceProvider), IReferencesController
{
    [HttpGet("{referenceId}")]
    public async Task<ReferenceDto> Get([FromRoute] int personId, [FromRoute] int referenceId)
    {
        return await referenceService.GetReference<ReferenceDto>(OrganizationId, personId, referenceId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ReferenceDto>> GetReferences([FromRoute] int personId)
    {
        return await referenceService.GetReferences<ReferenceDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ReferenceDto>> CreateReference([FromRoute] int personId,
        [FromBody] CreateReferenceOptions options)
    {
        var result = await referenceService.CreateReference(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{referenceId}")]
    public async Task<ActionResult<ReferenceDto>> UpdateReference([FromRoute] int personId,
        [FromRoute] int referenceId,
        [FromBody] ReferenceOptions options)
    {
        var result = await referenceService.UpdateReference(OrganizationId, personId, referenceId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{referenceId}")]
    public Task<Result> DeleteReference([FromRoute] int personId,
        [FromRoute] int referenceId)
    {
        return referenceService.DeleteReference(OrganizationId, personId, referenceId);
    }
}