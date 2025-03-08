#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Models;

public class ResumeSkillDto : IResumeSkill
{
    public string Title { get; set; }
    public virtual int Rating { get; set; }
    public string[] Categories { get; set; }

    [JsonIgnore] public int ResumeId { get; set; }

    public int SkillId { get; set; }

    [JsonIgnore] public int PersonId { get; set; }

    [JsonIgnore] public int OrganizationId { get; set; }
}