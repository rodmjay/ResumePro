#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public class OrganizationSettingsProxy : BaseProxy, IOrganizationSettingsController
{
    public OrganizationSettingsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ActionResult<OrganizationSettingsDto>> CreateSettings(OrganizationSettingsOptions options)
    {
        throw new NotImplementedException();
    }

    public async Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(OrganizationSettingsOptions options)
    {
        throw new NotImplementedException();
    }
}