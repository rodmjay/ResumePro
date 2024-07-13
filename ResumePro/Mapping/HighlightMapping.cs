#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;

namespace ResumePro.Mapping;

public class HighlightMapping : Profile
{
    public HighlightMapping()
    {
        CreateMap<Highlight, HighlightDto>();
    }
}