#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion


namespace ResumePro.Shared;

public class ResumeSkillDto : IResumeSkill
{
    [JsonIgnore]
    public int ResumeId { get; set; }

    [JsonIgnore]
    public  int SkillId { get; set; }
    public string Title { get; set; }
    public virtual int Rating { get; set; }

    [JsonIgnore]
    public int PersonaId { get; set; }
}