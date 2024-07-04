#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class PersonaMapping : Profile
{
    public PersonaMapping()
    {
        CreateMap<Persona, PersonaDto>()
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills.Count))
            .ForMember(x => x.Resumes, opt => opt.MapFrom(x => x.Resumes.Count))
            .IncludeAllDerived();

        CreateMap<Persona, PersonaDetails>()
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills.OrderByDescending(a => a.Rating)))
            .ForMember(x=>x.Resumes, opt=>opt.MapFrom(x=>x.Resumes))
            .ForMember(x=>x.Jobs, opt=>opt.MapFrom(x=>x.Jobs.OrderByDescending(a=>a.StartDate)));
    }
}