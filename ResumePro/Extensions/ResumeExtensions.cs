#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Extensions;

public static class ResumeExtensions
{
    public static Resume Create(this ResumeOptions options, int id, int organizationId, int personId)
    {
        var resume = new Resume
        {
            Id = id,
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonId = personId,
            JobTitle = options.JobTitle,
            Description = options.Description,
            ResumeSettings = new ResumeSettings
            {
                ObjectState = ObjectState.Added,
                DefaultTemplateId = options.Settings.DefaultTemplateId,
                OrganizationId = organizationId,
                ResumeYearHistory = options.Settings.ResumeYearHistory,
                ShowTechnologyPerJob = options.Settings.ShowTechnologyPerJob,
                AttachAllJobs = options.Settings.AttachAllJobs,
                AttachAllSkills = options.Settings.AttachAllSkills,
                ShowRatings = options.Settings.ShowRatings,
                ShowContactInfo = options.Settings.ShowContactInfo,
                ShowDuration = options.Settings.ShowDuration,
                SkillView = options.Settings.SkillView
            }
        };

        return resume;
    }

    public static Resume Update(this Resume resume, ResumeOptions options)
    {


        return resume;
    }
}