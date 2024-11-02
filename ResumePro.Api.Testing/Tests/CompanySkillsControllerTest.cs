#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using NUnit.Framework;
using ResumePro.Api.Controllers;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(CompanySkillsController))]
public class CompanySkillsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheAddCompanySkillsMethod : CompanySkillsControllerTest
    {
        [Test]
        public async Task CanAddJobSkills()
        {
            await PersonSkillsProxy.ToggleSkill(1, 99);

            var response = await CompanySkillsProxy.AddCompanySkill(1, 1, 99);
            Assert.That(response.Succeeded, Is.True);
        }
    }
}