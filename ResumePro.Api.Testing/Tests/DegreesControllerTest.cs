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
[TestOf(typeof(DegreesController))]
public class DegreesControllerTest : BaseApiTest
{
    [TestFixture]
    public class TheCreateDegreeMethod : DegreesControllerTest
    {
        [TestCaseSource(typeof(DegreeTestData), nameof(DegreeTestData.ValidOptions))]
        public async Task CanCreateCertification(DegreeOptions options)
        {
            var response = await DegreesProxy.CreateDegree(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

        }
    }
}