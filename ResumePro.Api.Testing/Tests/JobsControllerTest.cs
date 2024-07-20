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
[TestOf(typeof(JobsController))]
public class JobsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateJobMethod : JobsControllerTest
    {
        [TestCaseSource(typeof(JobTestData), nameof(JobTestData.ValidOptions))]
        public async Task CanCreateJob(JobOptions options)
        {
            var response = await JobsProxy.CreateJob(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var job = response.GetObject();

            Assert.That(job.Company, Is.EqualTo(options.Company));
            Assert.That(job.Description, Is.EqualTo(options.Description));
            Assert.That(job.StartDate, Is.EqualTo(options.StartDate));
            Assert.That(job.Location, Is.EqualTo(options.Location));
            Assert.That(job.EndDate, Is.EqualTo(options.EndDate));

            if (job.EndDate == null) Assert.That(job.DisplayEndDate, Is.EqualTo("Present"));
        }
    }

    [TestFixture]
    public sealed class TheUpdateJobMethod : JobsControllerTest
    {
        [TestCaseSource(typeof(JobTestData), nameof(JobTestData.ValidOptions))]
        public async Task CanUpdateJob(JobOptions options)
        {
            var response = await JobsProxy.CreateJob(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var jobId = response.GetObject().Id;

            options.Description += "_updated";

            var updateResponse = await JobsProxy.UpdateJob(1, jobId, options);
            Assert.That(updateResponse.Result is OkObjectResult, Is.True);

            var job = updateResponse.GetObject();
            
            Assert.That(job.Description, Is.EqualTo(options.Description));
        }
    }

    [TestFixture]
    public sealed class TheDeleteJobMethod : JobsControllerTest
    {
        [TestCaseSource(typeof(JobTestData), nameof(JobTestData.ValidOptions))]
        public async Task CanDeleteJob(JobOptions options)
        {
            var response = await JobsProxy.CreateJob(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var jobId = response.GetObject().Id;

            var deleteResponse = await JobsProxy.DeleteJob(1, jobId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheGetJobsMethod : JobsControllerTest
    {
        [Test]
        public async Task CanGetJobs()
        {
            var response = await JobsProxy.GetJobs(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public sealed class TheGetJobMethod : JobsControllerTest
    {
        [Test]
        public async Task CanGetJob()
        {
            var response = await JobsProxy.GetJob(1, 1);
            Assert.That(response.Id, Is.GreaterThan(0));
        }
    }
}