#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.TestData;

public class ResumeTestData
{
    public static IEnumerable<ResumeOptions> ValidOptions()
    {
        return new List<ResumeOptions>
        {
            new()
            {
                Description = "test",
                JobIds = null,
                Settings = new ResumeSettingsOptions
                {
                    AttachAllJobs = true,
                    AttachAllSkills = true
                },
                Title = "foo"
            }
        };
    }
}