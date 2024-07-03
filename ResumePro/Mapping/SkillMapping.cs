#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class SkillMapping : Profile
{
    public SkillMapping()
    {
        CreateMap<Skill, SkillDto>()
            .IncludeAllDerived();

        CreateMap<JobSkill, ResumeSkillDto>()
            .ForMember(x => x.SkillId, opt => opt.MapFrom(x => x.SkillId))
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Skill.Skill.Skill.Title))
            .ForMember(x => x.Rating, opt => opt.MapFrom(x => x.Skill.Skill.Rating))
            .IncludeAllDerived();

        CreateMap<JobSkill, JobSkillDto>();
    }
}