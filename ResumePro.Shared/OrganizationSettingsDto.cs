﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Enums;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Shared;

public class OrganizationSettingsDto : IOrganizationSettings
{
    public int OrganizationId { get; set; }
    public int ResumeYearHistory { get; set; }
    public bool AttachAllJobs { get; set; }
    public bool AttachAllSkills { get; set; }
    public string DefaultTemplateId { get; set; }
    public bool ShowTechnologyPerJob { get; set; }
    public bool ShowDuration { get; set; }
    public bool ShowContactInfo { get; set; }
    public SkillView SkillView { get; set; }
    public bool ShowRatings { get; set; }
}