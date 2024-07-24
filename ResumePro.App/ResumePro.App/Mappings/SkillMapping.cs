#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public class SkillMapping : Profile
{
    public SkillMapping()
    {
        CreateMap<PersonaSkillDto, PersonaSkillsOptions>()
            .ForMember(x=>x.SkillId, opt=>opt.MapFrom(x=>x.SkillId));
    }
    
    
}