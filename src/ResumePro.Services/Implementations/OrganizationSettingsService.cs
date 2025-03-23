using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

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
            .FirstAsync();
    }

    public async Task<OneOf<OrganizationSettingsDto, Result>> AddOrUpdateUpdateOrganizationSettings(int organizationId,
        OrganizationSettingsOptions options)
    {
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, Options: {@options}"), organizationId,
            options);

        // use default options if null
        options ??= new OrganizationSettingsOptions();

        var settings = await Settings.Where(x => x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (settings == null)
            settings = new OrganizationSettings
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId
            };
        else
            settings.ObjectState = ObjectState.Modified;

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
        if (changes > 0) return await GetOrganizationSettings(organizationId);

        return Result.Failed();
    }
}