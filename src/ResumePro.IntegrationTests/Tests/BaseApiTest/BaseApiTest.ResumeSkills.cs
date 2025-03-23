using NUnit.Framework;
using Bespoke.Shared.Common;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<Result> AssertAddResumeSkill(int personId, int resumeId, int skillId)
        {
            var result = await ResumeSkillsController.AddResumeSkill(personId, resumeId, skillId);
            Assert.That(result.Succeeded, "Failed to add resume skill");
            return result;
        }

        protected async Task<Result> AssertDeleteResumeSkill(int personId, int resumeId, int skillId)
        {
            var result = await ResumeSkillsController.DeleteResumeSkill(personId, resumeId, skillId);
            Assert.That(result.Succeeded, "Failed to delete resume skill");
            return result;
        }
    }
}
