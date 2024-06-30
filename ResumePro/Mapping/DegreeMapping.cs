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