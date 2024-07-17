#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
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
}