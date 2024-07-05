using AutoMapper;
using ResumePro.Languages.Entities;
using ResumePro.Languages.Models;

namespace ResumePro.Languages.Projections
{
    internal class LanguageProjections : Profile
    {
        public LanguageProjections()
        {
            CreateMap<Language, LanguageOutput>().IncludeAllDerived();
            

        }
    }
}
