#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Shared.Options;

public class ResumeSettingsOptions 
{
    public int ResumeYearHistory { get; set; }
    public string DefaultTemplateId { get; set; }
    public bool ShowTechnologyPerJob { get; set; }

    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
}