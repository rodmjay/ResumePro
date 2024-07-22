#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class PersonMapping : Profile
{
    public PersonMapping()
    {
        CreateMap<PersonaDetails, PersonaOptions>()
            .ForMember(x => x.StateId, opt => opt.MapFrom(x => x.StateId))
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.LastName))
            .ForMember(x => x.GitHub, opt => opt.MapFrom(x => x.GitHub))
            .ForMember(x => x.LinkedIn, opt => opt.MapFrom(x => x.LinkedIn))
            .ForMember(x => x.City, opt => opt.MapFrom(x => x.City))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Email));
    }
}