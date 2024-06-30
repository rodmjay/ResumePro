using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class JobMapping : Profile
{
    public JobMapping()
    {
        CreateMap<Job, JobDto>()
            .IncludeAllDerived();

        CreateMap<Job, JobDetails>()
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Highlighs));

        CreateMap<ResumeJob, JobDetails>()
            .ForMember(x => x.Company, opt => opt.MapFrom(x => x.Job.Company))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Job.Description))
            .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Job.Location))
            .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Job.Title))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills
                .OrderByDescending(a => a.Skill.Skill.Rating)))
            .ForMember(x => x.Projects, opt => opt.MapFrom(x => x.Job.Projects))
            .ForMember(x => x.Highlights, opt => opt.MapFrom(x => x.Job.Highlighs.OrderBy(a => a.Order).Where(a => a.ProjectId == null)));
    }
}