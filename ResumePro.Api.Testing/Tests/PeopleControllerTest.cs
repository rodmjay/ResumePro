using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Testing.Tests
{
    [TestFixture]
    public class PeopleControllerTest : BaseApiTest
    {
        [TestFixture]
        public class TheUpdatePersonMethod : PeopleControllerTest
        {
            [TestCaseSource(typeof(PersonCreateOptionsTestData), nameof(PersonCreateOptionsTestData.ValidOptions))]
            public async Task CanUpdatePerson(PersonaOptions options)
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
        public class TheDeletePersonMethod : PeopleControllerTest
        {
            [TestCaseSource(typeof(PersonCreateOptionsTestData), nameof(PersonCreateOptionsTestData.ValidOptions))]
            public async Task DeletePerson(PersonaOptions options)
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
        public class TheCreatePersonMethod : PeopleControllerTest
        {
            [TestCaseSource(typeof(PersonCreateOptionsTestData), nameof(PersonCreateOptionsTestData.ValidOptions))]
            public async Task CreatePerson(PersonaOptions options)
            {
                var result = await PeopleProxy.CreatePerson(options);
                ClassicAssert.IsNotNull(result);
            }
        }

        [TestFixture]
        public class TheGetPersonMethod : PeopleControllerTest
        {
            [Test]
            public async Task CanGetPerson()
            {
                var result = await PeopleProxy.GetPerson(1);
                ClassicAssert.IsNotNull(result);
            }
        }

        [TestFixture]
        public class TheGetPeopleMethod : PeopleControllerTest
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
}
