#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;

namespace ResumePro.Mapping;

public class ReferenceMapping : Profile
{
    public ReferenceMapping()
    {
        CreateMap<Reference, ReferenceDto>()
            .IncludeAllDerived();
    }
}