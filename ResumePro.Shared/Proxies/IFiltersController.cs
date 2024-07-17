#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Proxies;

public interface IFiltersController
{
    Task<FilterContainer> GetFilters();
}