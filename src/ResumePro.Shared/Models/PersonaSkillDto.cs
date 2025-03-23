#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class PersonaSkillDto : IPersonaSkill
{
    public string Name { get; set; } = null!;

    public string[] Categories { get; set; } = [];

    [JsonIgnore] public virtual int PersonId { get; set; }

    public virtual int SkillId { get; set; }
}