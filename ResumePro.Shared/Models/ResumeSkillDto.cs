#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared.Models;

public class ResumeSkillDto : IResumeSkill
{
    public string Title { get; set; }
    public virtual int Rating { get; set; }
    public string[] Categories { get; set; }

    [JsonIgnore] public int ResumeId { get; set; }

    public int SkillId { get; set; }

    [JsonIgnore] public int PersonaId { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }
}