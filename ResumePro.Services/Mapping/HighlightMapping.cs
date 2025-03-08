#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;


public class ProjectHighlightMapping : Profile
{
    public ProjectHighlightMapping()
    {
        CreateMap<ProjectHighlight, HighlightDto>();
    }
}

public class HighlightMapping : Profile
{

    public HighlightMapping()
    {
        CreateMap<Highlight, HighlightDto>();
    }
}