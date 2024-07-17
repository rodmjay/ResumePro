#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
public class JobsControllerTest : BaseApiTest
{
    [TestFixture]
    public class TheCreateJobMethod : JobsControllerTest
    {
        [TestCaseSource(typeof(JobOptionsTestData), nameof(JobOptionsTestData.ValidOptions))]
        public async Task CanCreateJob(JobOptions options)
        {
            var response = await JobsProxy.CreateJob(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }
}