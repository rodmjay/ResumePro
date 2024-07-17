#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ResumePro.Api.Testing.Extensions;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
public class SchoolControllerTest : BaseApiTest
{
    [TestFixture]
    public class TheCreateSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolOptionsTestData), nameof(SchoolOptionsTestData.ValidOptions))]
        public async Task CanCreateSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.CreateSchool(1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public class TheUpdateSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolOptionsTestData), nameof(SchoolOptionsTestData.ValidOptions))]
        public async Task CanUpdateSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.UpdateSchool(1,1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public class TheGetSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolOptionsTestData), nameof(SchoolOptionsTestData.ValidOptions))]
        public async Task CanGetSchool(SchoolOptions options)
        {
            var response = await SchoolsProxy.GetSchool(1, 1);
            Assert.That(response, Is.Not.Null);
        }
    }

    [TestFixture]
    public class TheGetSchoolsMethod : PeopleControllerTest
    {
        [Test]
        public async Task CanGetSchools()
        {
            var response = await SchoolsProxy.GetSchools(1);
            Assert.That(response.Count, Is.GreaterThan(0));
        }
    }

    [TestFixture]
    public class TheDeleteSchoolMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(SchoolOptionsTestData), nameof(SchoolOptionsTestData.ValidOptions))]
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