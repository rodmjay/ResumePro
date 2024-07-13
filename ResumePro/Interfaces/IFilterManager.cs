#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Interfaces;

public interface IFilterManager
{
    Task<FilterContainer> GetFilters(int organizationId);
}