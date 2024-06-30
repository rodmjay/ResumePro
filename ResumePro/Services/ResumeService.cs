using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Entities;
using ResumePro.Shared;

namespace ResumePro.Services
{
    public interface IResumeService : IService<Resume>
    {
        Task<T> GetResume<T>(int resumeId) where T : ResumeDto;
    }

    public class ResumeService : BaseService<Resume>, IResumeService
    {
        public ResumeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        private IQueryable<Resume> Resumes => Repository.Queryable();

        public Task<T> GetResume<T>(int resumeId) where T : ResumeDto
        {
            return Resumes.Where(x => x.Id == resumeId)
                .AsNoTracking()
                .ProjectTo<T>(ProjectionMapping)
                .FirstOrDefaultAsync();
        }
    }
}
