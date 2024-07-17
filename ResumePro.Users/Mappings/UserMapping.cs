#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserOutput>()
            .ForMember(x => x.OrganizationName, opt => opt.MapFrom(x => x.Organization.Name))
            .ForMember(x => x.OrganizationId, opt => opt.MapFrom(x => x.Organization.Id))
            .IncludeAllDerived();
    }
}