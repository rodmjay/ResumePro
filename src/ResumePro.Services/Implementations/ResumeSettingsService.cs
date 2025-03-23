using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ResumeSettingsService : BaseService<ResumeSettings>, IResumeSettingsService
{
    public ResumeSettingsService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<ResumeSettings> ResumeSettings => Repository.Queryable();

    public async Task<ResumeSettingsDto> GetResumeSettings(int organizationId, int personId, int resumeId)
    {
        return await ResumeSettings
            .AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.ResumeId == resumeId)
            .ProjectTo<ResumeSettingsDto>(Mapper)
            .FirstAsync();
    }

    public async Task<OneOf<ResumeSettingsDto, Result>> AddOrUpdateUpdateResumeSettings(int organizationId,
        int personId, int resumeId,
        ResumeSettingsOptions options)
    {
        var settings = await ResumeSettings.Where(x => x.OrganizationId == organizationId && x.ResumeId == resumeId)
            .FirstOrDefaultAsync();

        if (settings == null)
            settings = new ResumeSettings
            {
                ObjectState = ObjectState.Added,
                OrganizationId = organizationId,
                ResumeId = resumeId
            };
        else
            settings.ObjectState = ObjectState.Modified;

        settings.AttachAllJobs = options.AttachAllJobs;
        settings.ResumeYearHistory = options.ResumeYearHistory;
        settings.ShowContactInfo = options.ShowContactInfo;
        settings.ShowDuration = options.ShowDuration;
        settings.ShowRatings = options.ShowRatings;
        settings.ShowTechnologyPerJob = options.ShowTechnologyPerJob;
        settings.AttachAllSkills = options.AttachAllSkills;
        settings.DefaultTemplateId = options.DefaultTemplateId;
        settings.SkillView = options.SkillView;

        var records = Repository.InsertOrUpdateGraph(settings, true);
        if (records > 0) return await GetResumeSettings(organizationId, personId, resumeId);

        return Result.Failed();
    }
}