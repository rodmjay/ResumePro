#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Interfaces;

public interface IResumeSkill
{
    int ResumeId { get; set; }
    int SkillId { get; set; }
    int PersonId { get; set; }
    int OrganizationId { get; set; }
}