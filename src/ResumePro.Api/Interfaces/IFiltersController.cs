namespace ResumePro.Api.Interfaces;

public interface IFiltersController
{
    Task<FilterContainer> GetFilters();
}