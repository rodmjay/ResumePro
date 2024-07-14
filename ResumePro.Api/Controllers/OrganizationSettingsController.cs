#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc.ModelBinding;
using OneOf;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/settings")]
public sealed class OrganizationSettingsController : BaseController
{
    private readonly IOrganizationSettingsService _service;

    public OrganizationSettingsController(IServiceProvider serviceProvider, IOrganizationSettingsService service) : base(serviceProvider)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<OneOf<OrganizationSettingsDto, Result>> CreateSettings(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] OrganizationSettingsOptions? options)
    {
        return await _service.AddOrUpdateUpdateOrganizationSettings(OrganizationId, options)
            .ConfigureAwait(false);
    }

    [HttpPut]
    public async Task<OneOf<OrganizationSettingsDto, Result>> UpdateSettings(
        [FromBody] OrganizationSettingsOptions options)
    {
        return await _service.AddOrUpdateUpdateOrganizationSettings(OrganizationId, options)
            .ConfigureAwait(false);
    }
}