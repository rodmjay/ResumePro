using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<ReferenceDto> AssertGetReference(int personId, int referenceId)
        {
            var reference = await ReferencesController.Get(personId, referenceId);
            Assert.That(reference, Is.Not.Null, "Failed to retrieve reference");
            return reference;
        }

        protected async Task<List<ReferenceDto>> AssertGetReferences(int personId)
        {
            var references = await ReferencesController.GetReferences(personId);
            Assert.That(references, Is.Not.Null, "Failed to retrieve references");
            return references;
        }

        protected async Task<ReferenceDto> AssertCreateReference(int personId, ReferenceOptions options)
        {
            var response = await ReferencesController.CreateReference(personId, options);
            Assert.That(response.IsSuccessStatusCode(), "Reference creation failed");
            var reference = response.GetObject<ReferenceDto>();
            return reference;
        }

        protected async Task<ReferenceDto> AssertUpdateReference(int personId, int referenceId, ReferenceOptions options)
        {
            var response = await ReferencesController.UpdateReference(personId, referenceId, options);
            Assert.That(response.IsSuccessStatusCode(), "Reference update failed");
            var reference = response.GetObject<ReferenceDto>();
            return reference;
        }

        protected async Task<Result> AssertDeleteReference(int personId, int referenceId)
        {
            var result = await ReferencesController.DeleteReference(personId, referenceId);
            Assert.That(result.Succeeded, "Reference deletion failed");
            return result;
        }
    }
}
