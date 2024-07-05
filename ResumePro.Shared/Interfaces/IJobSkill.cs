#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IJobSkill
{
    int OrganizationId { get; set; }
    int JobId { get; set; }
    int PersonaId { get; set; }
    int SkillId { get; set; }
}