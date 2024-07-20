#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Enums;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class OrganizationSettingsTestData
{
    public static IEnumerable<OrganizationSettingsOptions> ValidOptions()
    {
        return new List<OrganizationSettingsOptions>
        {
            new()
            {
                AttachAllSkills = true,
                AttachAllJobs = true,
                DefaultTemplateId = 2,
                ResumeYearHistory = 10,
                ShowContactInfo = true,
                ShowDuration = true,
                ShowRatings = false,
                ShowTechnologyPerJob = true,
                SkillView = SkillView.Grouped
            }
        };
    }
}