﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Enums;

namespace ResumePro.Shared.Options;

public class OrganizationSettingsOptions
{
    public int ResumeYearHistory { get; set; } = 10;
    public string DefaultTemplateId { get; set; } = "markdown";
    public bool ShowTechnologyPerJob { get; set; } = false;
    public bool AttachAllJobs { get; set; } = true;
    public bool AttachAllSkills { get; set; } = true;
    public bool ShowRatings { get; set; } = false;
    public bool ShowDuration { get; set; } = true;
    public bool ShowContactInfo { get; set; } = false;
    public SkillView SkillView { get; set; } = SkillView.Grouped;
}