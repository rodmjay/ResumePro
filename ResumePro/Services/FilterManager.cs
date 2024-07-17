#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class FilterManager(
    IServiceProvider serviceProvider,
    ISkillService skillService,
    IStateService stateService)
    : BaseService(serviceProvider), IFilterManager
{
    public async Task<FilterContainer> GetFilters(int organizationId)
    {
        var statesTask = stateService.GetStatesDropdown("US");
        var skillsTask = skillService.GetSkillsDropdown();

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