using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class ResumeCompanyMapping : Profile
{
    public ResumeCompanyMapping()
    {
        CreateMap<ResumeCompany, CompanyDetails>()
            .ForMember(x => x.ShowTechnology, opt => opt.MapFrom(x => x.Resume.ResumeSettings.ShowTechnologyPerJob))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Company.Id))
            .ForMember(x => x.CompanyName, opt => opt.MapFrom(x => x.Company.CompanyName))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Company.Description))
            .ForMember(x => x.Location, opt => opt.MapFrom(x => x.Company.Location))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.Company.StartDate))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.Company.EndDate))
            .ForMember(x => x.Skills, opt => opt.MapFrom(x => x.Company.Skills))
            .ForMember(x => x.Positions,
                opt => opt.MapFrom(x => x.Company.Positions.OrderByDescending(p => p.StartDate)));
    }
}