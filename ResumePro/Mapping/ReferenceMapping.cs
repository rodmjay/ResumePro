#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ReferenceMapping : Profile
{
    public ReferenceMapping()
    {
        CreateMap<Reference, ReferenceDto>()
            .IncludeAllDerived();
    }
}