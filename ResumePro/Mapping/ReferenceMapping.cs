#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

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

        CreateMap<ResumeReference, ReferenceDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Reference.Name))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Reference.Id))
            .ForMember(x => x.Text, opt => opt.MapFrom(x => x.Reference.Text));
    }
}