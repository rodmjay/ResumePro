#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/filters")]
public class FiltersController : BaseController
{
    private readonly IFilterManager _filterManager;

    public FiltersController(IServiceProvider serviceProvider, IFilterManager filterManager) : base(serviceProvider)
    {
        _filterManager = filterManager;
    }

    [HttpGet]
    public async Task<FilterContainer> GetFilters()
    {
        return await _filterManager.GetFilters(OrganizationId);
    }
}