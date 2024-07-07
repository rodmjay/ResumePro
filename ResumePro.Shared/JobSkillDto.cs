#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class JobSkillDto : IJobSkill
{
    public string Name { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }

    [JsonIgnore] public int JobId { get; set; }

    [JsonIgnore] public int PersonaId { get; set; }

    public int SkillId { get; set; }
}