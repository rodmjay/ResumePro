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
    }
}