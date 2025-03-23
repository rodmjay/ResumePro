using NUnit.Framework;
using ResumePro.Shared.Models;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<List<PersonaSkillDto>> AssertGetPersonSkills(int personId)
        {
            var skills = await PersonSkillsController.GetSkills(personId);
            Assert.That(skills, Is.Not.Null, "Failed to retrieve person skills");
            return skills;
        }

        protected async Task<Result> AssertTogglePersonSkill(int personId, int skillId)
        {
            var result = await PersonSkillsController.ToggleSkill(personId, skillId);
            Assert.That(result.Succeeded, "Failed to toggle person skill");
            return result;
        }
    }
}
