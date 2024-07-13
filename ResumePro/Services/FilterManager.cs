#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class FilterManager : BaseService, IFilterManager
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
        var statesTask = _stateService.GetStatesDropdown("US");
        var skillsTask = _skillService.GetSkillsDropdown();

        await Task.WhenAll(statesTask, skillsTask);

        // Use the results of the tasks
        var retVal = new FilterContainer
        {
            States = statesTask.Result,
            Skills = skillsTask.Result
        };

        return retVal;
    }
}