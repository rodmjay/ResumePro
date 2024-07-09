#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Common;

namespace ResumePro.Interfaces;

public interface IStateService
{
    Task<List<DropdownItem>> GetStatesDropdown(string countryId);
}