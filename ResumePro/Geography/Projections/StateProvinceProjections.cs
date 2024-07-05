#region Header

// /*

// Author: Rod Johnson, Architect, rodmjay@gmail.com
// */

#endregion

using AutoMapper;
using ResumePro.Geography.Entities;
using ResumePro.Geography.Models;
using ResumePro.Shared.Common;

namespace ResumePro.Geography.Projections
{
    public class StateProvinceProjections : Profile
    {
        public StateProvinceProjections()
        {
            CreateMap<StateProvince, StateProvinceOutput>()
                .IncludeAllDerived();

            CreateMap<StateProvince, DropdownItem>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(x => x.Id));
        }
    }
}