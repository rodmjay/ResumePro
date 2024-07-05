#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class JobSkillMapping : Profile
{
    public JobSkillMapping()
    {
        CreateMap<JobSkill, JobSkillDto>()
            .ForMember(x => x.OrganizationId, opt => opt.Ignore())
            .ForMember(x => x.JobId, opt => opt.Ignore())
            .ForMember(x => x.PersonaId, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Skill.Skill.Title));
    }
}