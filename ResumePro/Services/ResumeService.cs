#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;

namespace ResumePro.Services;

public class ResumeService : BaseService<Resume>, IResumeService
{
    public ResumeService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Resume> Resumes => Repository.Queryable();

    public Task<T> GetResume<T>(int organizationId, int resumeId) where T : ResumeDto
    {
        return Resumes.Where(x => x.Id == resumeId && x.OrganizationId == organizationId)
            .AsNoTracking()
            .ProjectTo<T>(ProjectionMapping)
            .FirstOrDefaultAsync();
    }
}