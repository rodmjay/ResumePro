#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class StateProvinceOutput : IStateProvince
{
    public string Name { get; set; }

    public string Abbrev { get; set; }

    public string Code { get; set; }
}