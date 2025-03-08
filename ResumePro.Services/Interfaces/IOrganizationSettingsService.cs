#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IOrganizationSettingsService : IService<OrganizationSettings>
{
    Task<OrganizationSettingsDto> GetOrganizationSettings(int organizationId);

    Task<OneOf<OrganizationSettingsDto, Result>> AddOrUpdateUpdateOrganizationSettings(int organizationId,
        OrganizationSettingsOptions options);
}