using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class ResumeMapping : Profile
{
    public ResumeMapping()
    {
        CreateMap<Resume, ResumeDto>()
            .ForMember(x => x.Settings, opt => opt.MapFrom(x => x.ResumeSettings))
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Persona.FirstName))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Persona.LastName))
            .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Persona.Email))
            .ForMember(x => x.LinkedIn, opt => opt.MapFrom(x => x.Persona.LinkedIn))
            .ForMember(x => x.GitHub, opt => opt.MapFrom(x => x.Persona.GitHub))
            .ForMember(x => x.City, opt => opt.MapFrom(x => x.Persona.City))
            .ForMember(x => x.State, opt => opt.MapFrom(x => x.Persona.State.Code))
            .ForMember(x => x.Country, opt => opt.MapFrom(x => x.Persona.State.Country.Iso2))
            .ForMember(x => x.JobCount, opt => opt.MapFrom(x => x.Companies.Count))
            .ForMember(x => x.SkillCount, opt => opt.MapFrom(x => x.Skills.Count))
            .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.Persona.PhoneNumber))
            .IncludeAllDerived();

        CreateMap<Resume, ResumeDetails>()
            .ForMember(x => x.Companies,
                opt => opt.MapFrom(x => x.Companies.OrderByDescending(a => a.Company.StartDate)))
            .ForMember(x => x.References, opt => opt.MapFrom(x => x.Persona.References.OrderBy(a => a.Order)))
            .ForMember(x => x.Renderings, opt => opt.MapFrom(x => x.Renderings))
            .ForMember(x => x.Languages,
                opt => opt.MapFrom(x => x.Persona.Languages.OrderByDescending(a => a.Proficiency)))
            .ForMember(x => x.Certifications,
                opt => opt.MapFrom(x => x.Persona.Certifications.OrderByDescending(a => a.Date)))
            .ForMember(x => x.Education, opt => opt.MapFrom(x => x.Persona.Schools.OrderByDescending(a => a.StartDate)))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills));
    }
}