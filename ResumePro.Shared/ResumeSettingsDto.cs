#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class ResumeSettingsDto : IResumeSettings
{
    [JsonIgnore]
    public int OrganizationId { get; set; }

    [JsonIgnore]
    public int ResumeId { get; set; }
    public int ResumeYearHistory { get; set; }
    public int? DefaultTemplateId { get; set; }
    public bool ShowTechnologyPerJob { get; set; }
    public bool AttachAllJobs { get; set; }
    public bool AttachAllSkills { get; set; }
}