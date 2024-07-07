#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Projections;

public class UserProjections : Profile
{
    public UserProjections()
    {
        CreateMap<User, UserOutput>()
            .IncludeAllDerived();
    }
}