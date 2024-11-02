#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(CompaniesController))]
public class CompaniesControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateCompanyMethod : CompaniesControllerTest
    {
        [TestCaseSource(typeof(CompanyTestData), nameof(CompanyTestData.ValidOptions))]
        public async Task CanCreateJob(CompanyOptions options)
        {
            var response = await CompaniesProxy.CreateCompany(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var job = response.GetObject();

            Assert.That(job.CompanyName, Is.EqualTo(options.Company));
            Assert.That(job.Description, Is.EqualTo(options.Description));
            Assert.That(job.StartDate, Is.EqualTo(options.StartDate));
            Assert.That(job.Location, Is.EqualTo(options.Location));
            Assert.That(job.EndDate, Is.EqualTo(options.EndDate));

            if (job.EndDate == null) Assert.That(job.DisplayEndDate, Is.EqualTo("Present"));
        }
    }

    [TestFixture]
    public sealed class TheUpdateCompanyMethod : CompaniesControllerTest
    {
        [TestCaseSource(typeof(CompanyTestData), nameof(CompanyTestData.ValidOptions))]
        public async Task CanUpdateCompany(CompanyOptions options)
        {
            var response = await CompaniesProxy.CreateCompany(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var jobId = response.GetObject().Id;

            options.Description += "_updated";

            var updateResponse = await CompaniesProxy.UpdateCompany(1, jobId, options);
            Assert.That(updateResponse.Result is OkObjectResult, Is.True);

            var job = updateResponse.GetObject();
            
            Assert.That(job.Description, Is.EqualTo(options.Description));
        }
    }

    [TestFixture]
    public sealed class TheDeleteCompanyMethod : CompaniesControllerTest
    {
        [TestCaseSource(typeof(CompanyTestData), nameof(CompanyTestData.ValidOptions))]
        public async Task CanDeleteCompany(CompanyOptions options)
        {
            var response = await CompaniesProxy.CreateCompany(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var jobId = response.GetObject().Id;

            var deleteResponse = await CompaniesProxy.DeleteCompany(1, jobId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheGetCompaniesMethod : CompaniesControllerTest
    {
        [Test]
        public async Task CanGetCompanies()
        {
            var response = await CompaniesProxy.GetCompanies(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public sealed class TheGetCompanyMethod : CompaniesControllerTest
    {
        [Test]
        public async Task CanGetCompany()
        {
            var response = await CompaniesProxy.GetCompany(1, 1);
            Assert.That(response.Id, Is.GreaterThan(0));
        }
    }
}