#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared;

namespace ResumePro.Services;

public interface IFilterManager
{
    Task<FilterContainer> GetFilters(int organizationId);
}