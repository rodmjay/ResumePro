﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class OrganizationSettingsService : BaseService<OrganizationSettings>, IOrganizationSettingsService
{
    public OrganizationSettingsService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<OrganizationSettings> Settings => Repository.Queryable();


    public Task<OrganizationSettingsDto> GetOrganizationSettings(int organizationId)
    {
        return Settings.AsNoTracking().Where(x => x.OrganizationId == organizationId)
            .ProjectTo<OrganizationSettingsDto>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<OrganizationSettingsDto, Result>> AddOrUpdateUpdateOrganizationSettings(int organizationId, OrganizationSettingsOptions options)
    {
        // use default options if null
        options ??= new OrganizationSettingsOptions();

        var settings = await Settings.Where(x => x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (settings == null)
        {
            settings = new OrganizationSettings()
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId
            };
        }
        else
        {
            settings.ObjectState = ObjectState.Modified;
        }

        settings.AttachAllJobs = options.AttachAllJobs;
        settings.AttachAllSkills = options.AttachAllSkills;
        settings.DefaultTemplateId = options.DefaultTemplateId;
        settings.ResumeYearHistory = options.ResumeYearHistory;
        settings.ShowDuration = options.ShowDuration;
        settings.ShowRatings = options.ShowRatings;
        settings.ShowContactInfo = options.ShowContactInfo;
        settings.ShowTechnologyPerJob = options.ShowTechnologyPerJob;
        settings.SkillView = options.SkillView;

        var changes = Repository.InsertOrUpdateGraph(settings, true);
        if (changes > 0)
        {
            return await GetOrganizationSettings(organizationId);
        }

        return Result.Failed();
    }
}