#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public interface IOrganizationSettingsController
{
    Task<ActionResult<OrganizationSettingsDto>> CreateSettings(
        OrganizationSettingsOptions options);

    Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(
         OrganizationSettingsOptions options);
}