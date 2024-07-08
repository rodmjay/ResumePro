using ResumePro.Core.Data.Interfaces;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Services;

public class FilterManager : BaseService, IFilterManager
{
    private readonly IRepositoryAsync<StateProvince> _stateRepository;

    public FilterManager(IServiceProvider serviceProvider, 
        IRepositoryAsync<StateProvince> stateRepository) : base(serviceProvider)
    {
        _stateRepository = stateRepository;
    }

    public Task<FilterContainer> GetFilters(int organizationId)
    {
        return Task.FromResult(new FilterContainer());
    }
}