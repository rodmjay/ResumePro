#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.Extensions;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(ResumesController))]
public class ResumesControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateResumeMethod : ResumesControllerTest
    {
        [TestCaseSource(typeof(ResumeTestData), nameof(ResumeTestData.ValidOptions))]
        public async Task CanCreateResume(ResumeOptions options)
        {
            var response = await ResumeProxy.CreateResume(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheUpdateResumeMethod : ResumesControllerTest
    {
        [TestCaseSource(typeof(ResumeTestData), nameof(ResumeTestData.ValidOptions))]
        public async Task CanUpdateResume(ResumeOptions options)
        {
            var response = await ResumeProxy.UpdateResume(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheDeleteResumeMethod : ResumesControllerTest
    {
        [TestCaseSource(typeof(ResumeTestData), nameof(ResumeTestData.ValidOptions))]
        public async Task CanDeleteResume(ResumeOptions options)
        {
            var response = await ResumeProxy.CreateResume(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var resumeId = response.GetObject().Id;

            var deleteResponse = await ResumeProxy.DeleteResume(1, resumeId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheGetResumeMethod : ResumesControllerTest
    {
        [Test]
        public async Task CanGetResume()
        {
            var response = await ResumeProxy.GetResume(1, 1);
            Assert.That(response, Is.Not.Null);
        }
    }

    [TestFixture]
    public sealed class TheGetResumesMethod : ResumesControllerTest
    {
        [Test]
        public async Task CanGetResumes()
        {
            var response = await ResumeProxy.GetResumes(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }
}