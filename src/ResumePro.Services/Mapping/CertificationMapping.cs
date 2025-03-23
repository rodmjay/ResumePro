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