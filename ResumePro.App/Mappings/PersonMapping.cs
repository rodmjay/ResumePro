#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class LanguageMapping : Profile
{
    public LanguageMapping()
    {
        CreateMap<PersonaLanguageDto, PersonLanguageOptions>()
            .ForMember(x => x.LanguageId, opt => opt.MapFrom(x => x.Code3))
            .ForMember(x => x.Proficiency, opt => opt.MapFrom(x => x.Proficiency));
    }
}

public sealed class PersonMapping : Profile
{
    public PersonMapping()
    {
        CreateMap<PersonaDetails, PersonOptions>()
            .ForMember(x=>x.LanguageOptions, opt=>opt.MapFrom(x=>x.Languages));
    }
}