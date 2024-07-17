#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Shared.Proxies;

public class FiltersProxy(HttpClient httpClient) : BaseProxy(httpClient), IFiltersController
{
    public async Task<FilterContainer> GetFilters()
    {
        return await DoGet<FilterContainer>("v1.0/filters");
    }
}