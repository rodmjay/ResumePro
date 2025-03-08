#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class JobSkillMapping : Profile
{
    public JobSkillMapping()
    {
        CreateMap<CompanySkill, CompanySkillDto>()
            .ForMember(x => x.OrganizationId, opt => opt.Ignore())
            .ForMember(x => x.CompanyId, opt => opt.Ignore())
            .ForMember(x => x.PersonId, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Skill.Skill.Title));
    }
}