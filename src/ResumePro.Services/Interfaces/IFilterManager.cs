using ResumePro.Shared.Models;

namespace ResumePro.Services.Interfaces;

public interface IFilterManager
{
    Task<FilterContainer> GetFilters(int organizationId);
}