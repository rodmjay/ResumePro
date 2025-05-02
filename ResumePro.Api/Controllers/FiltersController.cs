#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/filters")]
public sealed class FiltersController(IServiceProvider serviceProvider, IFilterManager filterManager)
    : ExtendedBaseController(serviceProvider), IFiltersController
{
    [HttpGet]
    public async Task<FilterContainer> GetFilters()
    {
        return await filterManager.GetFilters(OrganizationId)
            .ConfigureAwait(false);
    }
}