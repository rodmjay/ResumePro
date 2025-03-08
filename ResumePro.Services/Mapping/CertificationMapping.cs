#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class CertificationMapping : Profile
{
    public CertificationMapping()
    {
        CreateMap<Certification, CertificationDto>()
            .IncludeAllDerived();
    }
}