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
[TestOf(typeof(DegreesController))]
public class DegreesControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheCreateDegreeMethod : DegreesControllerTest
    {
        [TestCaseSource(typeof(DegreeTestData), nameof(DegreeTestData.ValidOptions))]
        public async Task CanCreateDegree(DegreeOptions options)
        {
            var response = await DegreesProxy.CreateDegree(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var degree = response.GetObject();

            Assert.That(degree.Name, Is.EqualTo(options.Name));
        }
    }

    [TestFixture]
    public sealed class TheUpdateDegreeMethod : DegreesControllerTest
    {
        [TestCaseSource(typeof(DegreeTestData), nameof(DegreeTestData.ValidOptions))]
        public async Task CanUpdateDegree(DegreeOptions options)
        {
            var response = await DegreesProxy.CreateDegree(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var degreeId = response.GetObject().Id;

            options.Name += "_updated";
            
            var updateResponse = await DegreesProxy.UpdateDegree(1, 1, degreeId, options);
            Assert.That(updateResponse.Result is OkObjectResult, Is.True);

            var degree = updateResponse.GetObject();
            
            Assert.That(degree.Name, Is.EqualTo(options.Name));
        }
    }

    [TestFixture]
    public sealed class TheDeleteDegreeMethod : DegreesControllerTest
    {
        [TestCaseSource(typeof(DegreeTestData), nameof(DegreeTestData.ValidOptions))]
        public async Task CanUpdateDegree(DegreeOptions options)
        {
            var response = await DegreesProxy.CreateDegree(1, 1, options);
            Assert.That(response.Result is OkObjectResult, Is.True);

            var degreeId = response.GetObject().Id;

            var deleteResponse = await DegreesProxy.DeleteDegree(1, 1, degreeId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }
}