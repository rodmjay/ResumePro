#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ResumePro.Api.Controllers;
using ResumePro.Api.Testing.TestData;
using ResumePro.Shared.Common;
using ResumePro.Shared.Extensions;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests;

[TestFixture]
[TestOf(typeof(PeopleController))]
public class PeopleControllerTest : BaseApiTest
{
    [TestFixture]
    public sealed class TheUpdatePersonMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(PersonTestData), nameof(PersonTestData.ValidOptions))]
        public async Task CanUpdatePerson(PersonOptions options)
        {
            var createResponse = await PeopleProxy.CreatePerson(options);
            Assert.That(createResponse.Result is OkObjectResult, Is.True);

            var personId = createResponse.GetObject().Id;

            Assert.That(personId, Is.GreaterThan(0));

            var updateResponse = await PeopleProxy.UpdatePerson(personId, options);
            Assert.That(updateResponse.Result is OkObjectResult, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheDeletePersonMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(PersonTestData), nameof(PersonTestData.ValidOptions))]
        public async Task DeletePerson(PersonOptions options)
        {
            var createResponse = await PeopleProxy.CreatePerson(options);
            Assert.That(createResponse.Result is OkObjectResult, Is.True);

            var personId = createResponse.GetObject().Id;

            Assert.That(personId, Is.GreaterThan(0));

            var deleteResponse = await PeopleProxy.DeletePerson(personId);
            Assert.That(deleteResponse.Succeeded, Is.True);
        }
    }

    [TestFixture]
    public sealed class TheCreatePersonMethod : PeopleControllerTest
    {
        [TestCaseSource(typeof(PersonTestData), nameof(PersonTestData.ValidOptions))]
        public async Task CreatePerson(PersonOptions options)
        {
            var result = await PeopleProxy.CreatePerson(options);
            ClassicAssert.IsNotNull(result);
        }
    }

    [TestFixture]
    public sealed class TheGetPersonMethod : PeopleControllerTest
    {
        [Test]
        public async Task CanGetPerson()
        {
            var result = await PeopleProxy.GetPerson(1);
            ClassicAssert.IsNotNull(result);
        }
    }

    [TestFixture]
    public sealed class TheGetPeopleMethod : PeopleControllerTest
    {
        [Test]
        public async Task CanGetPeople()
        {
            var result = await PeopleProxy.GetPeople(null, new PagingQuery());
            ClassicAssert.IsNotNull(result);

            ClassicAssert.AreEqual(result.TotalItems, 1);
        }
    }
}