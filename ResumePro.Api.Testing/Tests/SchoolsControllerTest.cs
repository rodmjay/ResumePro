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
[TestOf(typeof(SchoolsController))]
public class SchoolsControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolTestData), nameof(SchoolTestData.ValidOptions))]
        public async Task CanCreateSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.CreateSchool(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheUpdateSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolTestData), nameof(SchoolTestData.ValidOptions))]
        public async Task CanUpdateSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.UpdateSchool(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheGetSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolTestData), nameof(SchoolTestData.ValidOptions))]
        public async Task CanGetSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.GetSchool(1, 1);
            Assert.That(response, Is.Not.Null);
        }
    }

    [TestFixture]
    public sealed class TheGetSchoolsMethod : PeopleControllerTest
    {
        [Test]
        public async Task CanGetSchools()
        {
            var response = await SchoolsProxy.GetSchools(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public sealed class TheDeleteSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolTestData), nameof(SchoolTestData.ValidOptions))]
        public async Task CanDeleteSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.CreateSchool(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var schoolId = response.GetObject().Id;

            var deleteResponse = await SchoolsProxy.DeleteSchool(1, schoolId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }
}