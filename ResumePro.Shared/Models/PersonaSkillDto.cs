#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion


#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Models;

public class PersonaSkillDto : IPersonaSkill
{
    public string Name { get; set; }

    public string[] Categories { get; set; }

    [JsonIgnore] public virtual int PersonaId { get; set; }

    public virtual int SkillId { get; set; }

}