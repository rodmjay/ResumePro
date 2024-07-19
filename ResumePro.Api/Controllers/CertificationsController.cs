#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/certifications")]
public sealed class CertificationsController(
    IServiceProvider serviceProvider,
    ICertificationService certificationService)
    : BaseController(serviceProvider), ICertificationsController
{
    [HttpGet("{certificationId}")]
    public async Task<CertificationDto> Get([FromRoute] int personId, [FromRoute] int certificationId)
    {
        return await certificationService.GetCertification<CertificationDto>(OrganizationId, personId, certificationId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<CertificationDto>> Get([FromRoute] int personId)
    {
        return await certificationService.GetCertifications<CertificationDto>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<CertificationDto>> CreateCertification([FromRoute] int personId,
        [FromBody] CertificationOptions options)
    {
        var result = await certificationService.CreateCertification(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);
        return BadRequest(result.AsT1);
    }

    [HttpPut("{certificationId}")]
    public async Task<ActionResult<CertificationDto>> Update([FromRoute] int personId, [FromRoute] int certificationId,
        [FromBody] CertificationOptions options)
    {
        var result = await certificationService.UpdateCertification(OrganizationId, personId, certificationId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);
        return BadRequest(result.AsT1);
    }

    [HttpDelete("{certificationId}")]
    public async Task<Result> Delete([FromRoute] int personId, [FromRoute] int certificationId)
    {
        return await certificationService.DeleteCertification(OrganizationId, personId, certificationId)
            .ConfigureAwait(false);
    }
}