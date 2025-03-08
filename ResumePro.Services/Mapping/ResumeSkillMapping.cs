#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class ResumeSkillMapping : Profile
{
    public ResumeSkillMapping()
    {
        CreateMap<ResumeSkill, ResumeSkillDto>()
            .ForMember(x => x.Categories,
                opt => opt.MapFrom(x => x.Skill.Skill.Categories.Select(a => a.SkillCategory.Name)))
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Skill.Skill.Title));
    }
}