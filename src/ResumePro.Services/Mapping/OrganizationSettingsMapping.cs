using AutoMapper;
using ResumePro.Shared.Models;

namespace ResumePro.Services.Mapping;

public class OrganizationSettingsMapping : Profile
{
    public OrganizationSettingsMapping()
    {
        CreateMap<OrganizationSettings, OrganizationSettingsDto>();
    }
}