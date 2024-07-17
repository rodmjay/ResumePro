#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
public class ResumeControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateResumeMethod : ResumeControllerTest
    {
        [TestCaseSource(typeof(ResumeOptionsTestData), nameof(ResumeOptionsTestData.ValidOptions))]
        public async Task CanCreateResume(ResumeOptions options)
        {
            var response = await ResumeProxy.CreateResume(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheGetResumeMethod : ResumeControllerTest
    {
        [Test]
        public async Task CanGetResume()
        {
            var response = await ResumeProxy.GetResume(1, 1);
            Assert.That(response, Is.Not.Null);
        }
    }

    [TestFixture]
    public sealed class TheGetResumesMethod : ResumeControllerTest
    {
        [Test]
        public async Task CanGetResumes()
        {
            var response = await ResumeProxy.GetResumes(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }
}