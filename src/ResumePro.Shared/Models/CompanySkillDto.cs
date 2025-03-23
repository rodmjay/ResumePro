#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class CompanySkillDto : ICompanySkill
{
    public string Name { get; set; } = null!;

    [JsonIgnore] public int OrganizationId { get; set; }

    [JsonIgnore] public int CompanyId { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    public int SkillId { get; set; }
}