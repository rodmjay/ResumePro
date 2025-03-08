#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class DegreeMapping : Profile
{
    public DegreeMapping()
    {
        CreateMap<Degree, DegreeDto>()
            .IncludeAllDerived();
    }
}