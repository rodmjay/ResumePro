using NUnit.Framework;
using NUnit.Framework.Legacy;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;

namespace ResumePro.Api.Testing.Tests
{
    [TestFixture]
    public class PeopleControllerTest : BaseApiTest
    {



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
