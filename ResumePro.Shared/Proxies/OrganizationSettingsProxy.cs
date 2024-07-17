#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class OrganizationSettingsProxy(HttpClient httpClient) : BaseProxy(httpClient), IOrganizationSettingsController
{
    public async Task<ActionResult<OrganizationSettingsDto>> CreateSettings(OrganizationSettingsOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(OrganizationSettingsOptions options)
    {
        throw new NotImplementedException();
    }
}