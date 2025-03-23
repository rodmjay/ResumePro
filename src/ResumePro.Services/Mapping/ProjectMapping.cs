using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.CompanyId, opt => opt.Ignore())
            .IncludeAllDerived();

        CreateMap<Project, ProjectDetails>()
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Highlights.OrderBy(a => a.Order)));
    }
}