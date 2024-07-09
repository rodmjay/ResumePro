#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;

namespace ResumePro.Services;

public class FilterManager : BaseService, IFilterManager
{
    private readonly ISkillService _skillService;
    private readonly IStateService _stateService;

    public FilterManager(IServiceProvider serviceProvider,
        ISkillService skillService,
        IStateService stateService) : base(serviceProvider)
    {
        _skillService = skillService;
        _stateService = stateService;
    }

    public async Task<FilterContainer> GetFilters(int organizationId)
    {
        var retVal = new FilterContainer
        {
            States = await _stateService.GetStatesDropdown("US"),
            Skills = await _skillService.GetSkillsDropdown()
        };

        return retVal;
    }
}