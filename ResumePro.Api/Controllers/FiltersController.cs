#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Proxies;

namespace ResumePro.Api.Controllers;

[Route("v1.0/filters")]
public sealed class FiltersController(IServiceProvider serviceProvider, IFilterManager filterManager)
    : BaseController(serviceProvider), IFiltersController
{
    [HttpGet]
    public async Task<FilterContainer> GetFilters()
    {
        return await filterManager.GetFilters(OrganizationId);
    }
}