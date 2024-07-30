#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

[Route("v1.0/certifications")]
public sealed class CertificationsController(
    IServiceProvider serviceProvider,
    ICertificationService certificationService)
    : BaseController(serviceProvider), IIndividualCertificationsController
{
    [HttpGet("{certificationId}")]
    public async Task<CertificationDto> Get([FromRoute] int certificationId)
    {
        return await certificationService.GetCertification<CertificationDto>(OrganizationId, UserId, certificationId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<CertificationDto>> Get()
    {
        return await certificationService.GetCertifications<CertificationDto>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<CertificationDto>> CreateCertification(
        [FromBody] CertificationOptions options)
    {
        var result = await certificationService.CreateCertification(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);
        return BadRequest(result.AsT1);
    }

    [HttpPut("{certificationId}")]
    public async Task<ActionResult<CertificationDto>> Update( [FromRoute] int certificationId,
        [FromBody] CertificationOptions options)
    {
        var result = await certificationService.UpdateCertification(OrganizationId, UserId, certificationId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);
        return BadRequest(result.AsT1);
    }

    [HttpDelete("{certificationId}")]
    public async Task<Result> Delete( [FromRoute] int certificationId)
    {
        return await certificationService.DeleteCertification(OrganizationId, UserId, certificationId)
            .ConfigureAwait(false);
    }
}