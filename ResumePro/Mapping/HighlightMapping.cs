using AutoMapper;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Mapping;

public class HighlightMapping : Profile
{
    public HighlightMapping()
    {
        CreateMap<Highlight, HighlightDto>();
    }
}