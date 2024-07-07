#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared.Common;

namespace ResumePro.Services;

public class ResumeSkillService : BaseService<ResumeSkill>, IResumeSkillService
{
    public ResumeSkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<ResumeSkill> ResumeSkills => Repository.Queryable();

    public async Task<Result> AddResumeSkill(int organizationId, int personId, int resumeId,
        int skillId)
    {
        var resumeSkill = await ResumeSkills.Where(x => x.OrganizationId == organizationId
                                                        && x.PersonaId == personId && x.SkillId == skillId)
            .FirstOrDefaultAsync();

        if (resumeSkill != null) return Result.Failed();

        resumeSkill = new ResumeSkill
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonaId = personId,
            ResumeId = resumeId
        };

        var changes = Repository.InsertOrUpdateGraph(resumeSkill, true);
        if (changes > 0) return Result.Success();

        return Result.Failed();
    }

    public Task<Result> DeleteResumeSkill(int organizationId, int personId, int resumeId, int skillId)
    {
        throw new NotImplementedException();
    }
}