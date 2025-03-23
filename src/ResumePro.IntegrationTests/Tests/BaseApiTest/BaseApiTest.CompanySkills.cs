using NUnit.Framework;
using ResumePro.Shared.Models;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<Result> AssertAddCompanySkill(int personId, int companyId, int skillId)
        {
            var result = await CompanySkillsController.AddCompanySkill(personId, companyId, skillId);
            Assert.That(result.Succeeded, "Failed to add company skill");
            return result;
        }

        protected async Task<Result> AssertDeleteCompanySkill(int personId, int companyId, int skillId)
        {
            var result = await CompanySkillsController.DeleteCompanySkill(personId, companyId, skillId);
            Assert.That(result.Succeeded, "Failed to delete company skill");
            return result;
        }
    }
}
