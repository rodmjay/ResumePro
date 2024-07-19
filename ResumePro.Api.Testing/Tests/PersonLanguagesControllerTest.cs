#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using NUnit.Framework;
using ResumePro.Api.Controllers;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(PersonLanguagesController))]
public class PersonLanguagesControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheGetPersonLanguagesMethod : PersonLanguagesControllerTest
    {
        [Test]
        public async Task CanGetPersonLanguages()
        {
            var response = await PersonLanguagesProxy.GetPersonLanguages(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }
}