using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<Resume, ResumeDto>()
            .IncludeAllDerived();

        CreateMap<Resume, ResumeDetails>()
            .ForMember(x => x.Jobs, opt => opt.MapFrom(x => x.Jobs.OrderBy(a => a.Job.StartDate)))
            .ForMember(x => x.References, opt => opt.MapFrom(x => x.Jobs.SelectMany(a => a.Job.References)))
            .ForMember(x => x.Education, opt => opt.MapFrom(x => x.Persona.Schools))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills.OrderByDescending(a => a.Skill.Rating).Where(a => a.ShowInSummary == true)));
    }
}