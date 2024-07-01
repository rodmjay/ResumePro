using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<Resume, ResumeDto>()
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Persona.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Persona.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Persona.Email))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Persona.PhoneNumber))
            .IncludeAllDerived();

        CreateMap<Resume, ResumeDetails>()
            .ForMember(x => x.Jobs, opt => opt.MapFrom(x => x.Jobs.OrderByDescending(a => a.Job.StartDate)))
            .ForMember(x => x.References, opt => opt.MapFrom(x => x.Jobs.SelectMany(a => a.Job.References)))
            .ForMember(x => x.Education, opt => opt.MapFrom(x => x.Persona.Schools))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills.OrderByDescending(a => a.Skill.Rating).Where(a => a.ShowInSummary == true)));
    }
}