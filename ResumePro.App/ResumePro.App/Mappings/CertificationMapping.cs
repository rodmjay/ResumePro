#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.App.Mappings;

public sealed class CertificationMapping : Profile
{
    public CertificationMapping()
    {
        CreateMap<CertificationDto, CertificationOptions>()
            .ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date));
    }
}