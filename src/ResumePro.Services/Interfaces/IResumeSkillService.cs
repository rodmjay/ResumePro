using Bespoke.Services.Interfaces;

namespace ResumePro.Services.Interfaces;

public interface IResumeSkillService : IService<ResumeSkill>
{
    Task<Result> AddResumeSkill(int organizationId, int personId, int resumeId, int skillId);

    Task<Result> DeleteResumeSkill(int organizationId, int personId, int resumeId, int skillId);
}