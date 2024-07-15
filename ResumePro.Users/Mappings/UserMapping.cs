#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserOutput>()
            .IncludeAllDerived();
    }
}