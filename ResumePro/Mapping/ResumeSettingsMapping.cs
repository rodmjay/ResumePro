#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ResumeSettingsMapping : Profile
{
    public ResumeSettingsMapping()
    {
        CreateMap<ResumeSettings, ResumeSettingsDto>();
    }
}