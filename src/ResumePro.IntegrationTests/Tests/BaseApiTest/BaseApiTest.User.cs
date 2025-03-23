using NUnit.Framework;
using ResumePro.Shared.Models;
using Bespoke.IntegrationTesting.Extensions;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<UserOutput> AssertGetUser()
        {
            var user = await UserController.GetUser();
            Assert.That(user, Is.Not.Null, "Failed to retrieve user");
            return user;
        }
    }
}
