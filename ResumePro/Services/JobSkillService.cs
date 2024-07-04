using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;

namespace ResumePro.Services;

public class JobSkillService : BaseService<JobSkill>, IJobSkillService
{
    public JobSkillService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}