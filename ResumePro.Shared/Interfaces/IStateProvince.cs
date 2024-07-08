#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IStateProvince
{
    string Name { get; set; }
    string Abbrev { get; set; }
    string Code { get; set; }
}