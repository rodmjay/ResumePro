﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Languages.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class FilterManager(
    IServiceProvider serviceProvider,
    ISkillService skillService,
    ILanguageService languageService,
    IStateService stateService)
    : BaseService(serviceProvider), IFilterManager
{
    public async Task<FilterContainer> GetFilters(int organizationId)
    {
        var retVal = new FilterContainer
        {
            States = await stateService.GetStatesDropdown("US"),
            Skills = await skillService.GetSkillsDropdown(),
            Languages = await languageService.GetLanguageDropdown()
        };

        return retVal;
    }
}