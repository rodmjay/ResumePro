﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Models;
using ResumePro.Shared.Models;

namespace ResumePro.Languages.Projections;

internal class LanguageProjections : Profile
{
    public LanguageProjections()
    {
        CreateMap<Language, LanguageDto>().IncludeAllDerived();
    }
}