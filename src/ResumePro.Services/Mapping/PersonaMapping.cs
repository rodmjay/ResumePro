using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class PersonaMapping : Profile
{
    public PersonaMapping()
    {
        CreateMap<Persona, PersonaDto>()
            .ForMember(x => x.SkillCount, opt => opt.MapFrom(x => x.Skills.Count))
            .ForMember(x => x.ResumeCount, opt => opt.MapFrom(x => x.Resumes.Count))
            .ForMember(x => x.CertificationCount, opt => opt.MapFrom(x => x.Certifications.Count))
            .ForMember(x => x.JobCount, opt => opt.MapFrom(x => x.Companies.Count))
            .ForMember(x => x.ReferencesCount, opt => opt.MapFrom(x => x.References.Count))
            .ForMember(x => x.State, opt => opt.MapFrom(x => x.State.Code))
            .ForMember(x => x.StateId, opt => opt.MapFrom(x => x.State.Id))
            .ForMember(x => x.Country, opt => opt.MapFrom(x => x.State.Country.Iso2))
            .IncludeAllDerived();

        CreateMap<Persona, PersonaDetails>()
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Skills))
            .ForMember(x => x.Certifications, opt => opt.MapFrom(x => x.Certifications.OrderByDescending(a => a.Date)))
            .ForMember(x => x.Education, opt => opt.MapFrom(x => x.Schools.OrderByDescending(a => a.StartDate)))
            .ForMember(x => x.Languages, opt => opt.MapFrom(x => x.Languages.OrderByDescending(a => a.Proficiency)))
            .ForMember(x => x.Resumes, opt => opt.MapFrom(x => x.Resumes))
            .ForMember(x => x.References, opt => opt.MapFrom(x => x.References.OrderBy(r => r.Order)))
            .ForMember(x => x.Jobs, opt => opt.MapFrom(x => x.Companies.OrderByDescending(a => a.StartDate)));
    }
}