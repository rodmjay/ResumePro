#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public sealed class TemplateMapping : Profile
{
    public TemplateMapping()
    {
        CreateMap<Template, TemplateDto>()
            .ForMember(x => x.IsGlobal, opt => opt.MapFrom(x => x.OrganizationId == null))
            .IncludeAllDerived();
    }
}