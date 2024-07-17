#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc.ModelBinding;
using OneOf;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/settings")]
public sealed class OrganizationSettingsController(
    IServiceProvider serviceProvider,
    IOrganizationSettingsService service)
    : BaseController(serviceProvider), IOrganizationSettingsController
{
    [HttpPost]
    public async Task<ActionResult<OrganizationSettingsDto>> CreateSettings(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] OrganizationSettingsOptions? options)
    {
        var result = await service.AddOrUpdateUpdateOrganizationSettings(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpPut]
    public async Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(
        [FromBody] OrganizationSettingsOptions options)
    {
        var result = await service.AddOrUpdateUpdateOrganizationSettings(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }
}