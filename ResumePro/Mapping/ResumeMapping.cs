#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<Resume, ResumeDto>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Persona.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Persona.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Persona.Email))
            .ForMember(x => x.LinkedIn, opt => opt.MapFrom(x => x.Persona.LinkedIn))
            .ForMember(x => x.GitHub, opt => opt.MapFrom(x => x.Persona.GitHub))
            .ForMember(x => x.JobTitle, opt => opt.MapFrom(x => x.JobTitle))
            .ForMember(x => x.City, opt => opt.MapFrom(x => x.Persona.City))
            .ForMember(x => x.State, opt => opt.MapFrom(x => x.Persona.State.Code))
            .ForMember(x => x.Country, opt => opt.MapFrom(x => x.Persona.State.Country.Iso2))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Persona.PhoneNumber))
            .IncludeAllDerived();

        CreateMap<Resume, ResumeDetails>()
            .ForMember(x => x.Jobs, opt => opt
                .MapFrom(x => x.Jobs.OrderByDescending(a => a.Job.StartDate)))
            .ForMember(x => x.References, opt => opt
                .MapFrom(x => x.References))
            .ForMember(x=>x.Languages, opt=>opt.MapFrom(x=>x.Persona.Languages))
            .ForMember(x => x.Education, opt => opt
                .MapFrom(x => x.Persona.Schools))
            .ForMember(x => x.Skills, opt => opt
                .MapFrom(x => x.Skills.OrderByDescending(a => a.Skill.Rating)));
    }
}