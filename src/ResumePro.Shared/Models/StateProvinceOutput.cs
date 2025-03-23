#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class StateProvinceOutput : IStateProvince
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string Abbrev { get; set; } = null!;

    public string Code { get; set; } = null!;
}