using ResumePro.Shared;

namespace ResumePro.Services;

public interface IFilterManager
{
    Task<FilterContainer> GetFilters(int organizationId);
}