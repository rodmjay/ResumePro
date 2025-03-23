using NUnit.Framework;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<ChatResult> AssertProfessionalize(ChatOptions options)
        {
            var result = await TextController.Professionalize(options);
            Assert.That(result, Is.Not.Null, "Failed to professionalize text");
            return result;
        }
    }
}
