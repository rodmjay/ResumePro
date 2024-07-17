#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public class FiltersProxy : BaseProxy, IFiltersController
{
    public FiltersProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<FilterContainer> GetFilters()
    {
        throw new NotImplementedException();
    }
}