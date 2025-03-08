#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Interfaces;

public interface IOrganizationSettingsController
{
    Task<ActionResult<OrganizationSettingsDto>> CreateSettings(
        OrganizationSettingsOptions options);

    Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(
        OrganizationSettingsOptions options);
}