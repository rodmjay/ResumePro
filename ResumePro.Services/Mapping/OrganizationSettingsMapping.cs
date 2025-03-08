#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class OrganizationSettingsMapping : Profile
{
    public OrganizationSettingsMapping()
    {
        CreateMap<OrganizationSettings, OrganizationSettingsDto>();
    }
}