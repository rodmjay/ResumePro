using NUnit.Framework;
using ResumePro.Shared.Models;

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
