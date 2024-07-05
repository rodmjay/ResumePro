#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class PersonalSkillMapping : Profile
{
    public PersonalSkillMapping()
    {
        CreateMap<PersonaSkill, PersonaSkillDto>()
            .ForMember(x => x.PersonaId, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Skill.Title));
    }
}