#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Services.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/filters")]
public sealed class FiltersController : BaseController, IFiltersController
{
    private readonly IFilterManager _filterManager;

    public FiltersController(IServiceProvider serviceProvider, IFilterManager filterManager) : base(serviceProvider)
    {
        _filterManager = filterManager;
    }

    [HttpGet]
    public async Task<FilterContainer> GetFilters()
    {
        return await _filterManager.GetFilters(OrganizationId)
            .ConfigureAwait(false);
    }
}