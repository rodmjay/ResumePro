#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class FilterContainer
{
    public List<SkillDto> Skills { get; set; } = new();
    public List<StateProvinceOutput> States { get; set; } = new();
    public List<LanguageDto> Languages { get; set; } = new();
}