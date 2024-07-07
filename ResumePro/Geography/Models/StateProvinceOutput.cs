#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Geography.Interfaces;

namespace ResumePro.Geography.Models;

public class StateProvinceOutput : IStateProvince
{
    public string Name { get; set; }

    public string Abbrev { get; set; }

    public string Code { get; set; }
}