#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using NUnit.Framework;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(PersonSkillsController))]
public class PersonSkillsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheGetSkillsMethod : PersonSkillsControllerTest
    {
        [Test]
        public async Task CanGetSkills()
        {
            var response = await PersonSkillsProxy.GetSkills(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public sealed class TheAddOrUpdateSkillMethod : PersonSkillsControllerTest
    {
        [TestCaseSource(typeof(PersonSkillTestData), nameof(PersonSkillTestData.ValidOptions))]
        public async Task CanAddOrUpdateSkill(PersonaSkillsOptions options)
        {
            var response = await PersonSkillsProxy.AddOrUpdateSkill(1, options);
            Assert.That(response.Succeeded, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheDeletePersonalSkillMethod : PersonSkillsControllerTest
    {
        [TestCaseSource(typeof(PersonSkillTestData), nameof(PersonSkillTestData.ValidOptions))]
        public async Task CanDeleteSkill(PersonaSkillsOptions options)
        {
            var response = await PersonSkillsProxy.AddOrUpdateSkill(1, options);
            Assert.That(response.Succeeded, Is.True);

            var deleteResponse = await PersonSkillsProxy.DeletePersonalSkill(1, options.SkillId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }
}