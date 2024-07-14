#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;

namespace ResumePro.Interfaces;

public interface IOrganizationSettingsService : IService<OrganizationSettings>
{
    Task<OrganizationSettingsDto> GetOrganizationSettings(int organizationId);
    Task<OneOf<OrganizationSettingsDto, Result>> AddOrUpdateUpdateOrganizationSettings(int organizationId,
        OrganizationSettingsOptions options);
}