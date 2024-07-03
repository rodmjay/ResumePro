#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class DegreeMapping : Profile
{
    public DegreeMapping()
    {
        CreateMap<Degree, DegreeDto>();
    }
}