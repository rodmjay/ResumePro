#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared;

public interface IResumeSkill
{
    int ResumeId { get; set; }
    int SkillId { get; set; }
    int PersonaId { get; set; }
}