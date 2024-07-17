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
[TestOf(typeof(ProjectsController))]
public class ProjectsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateProjectMethod : ProjectsControllerTest
    {
        [TestCaseSource(typeof(ProjectTestData), nameof(ProjectTestData.ValidOptions))]
        public async Task CanCreateProject(ProjectOptions options)
        {
            var response = await ProjectsProxy.Create(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }
}