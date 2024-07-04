#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IPersonaSkill
{
    int PersonaId { get; set; }
    int SkillId { get; set; }
    int Rating { get; set; }
}