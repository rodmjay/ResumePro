#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using NUnit.Framework;
using ResumePro.Api.Controllers;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(SkillsController))]
public class SkillsControllerTest : BaseApiTest
{
    [TestFixture]
    public class TheGetSkillsMethod : SkillsControllerTest
    {
        [Test]
        public async Task CanGetSkills()
        {
            var response = await SkillsProxy.GetSkills();
            Assert.That(response, Is.Not.Null);
        }
    }
}