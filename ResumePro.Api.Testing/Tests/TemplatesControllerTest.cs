#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(TemplatesController))]
public class TemplatesControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheGetTemplatesMethod : TemplatesControllerTest
    {
        [Test]
        public async Task CanGetTemplates()
        {
            var response = await TemplatesProxy.GetTemplates();
            Assert.That(response, Is.Not.Null);
        }
    }

    [TestFixture]
    public sealed class TheCreateTemplateMethod : TemplatesControllerTest
    {
        [TestCaseSource(typeof(TemplatesTestData), nameof(TemplatesTestData.ValidOptions))]
        public async Task CanCreateTemplate(TemplateOptions options)
        {
            var response = await TemplatesProxy.CreateTemplate(options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }
}