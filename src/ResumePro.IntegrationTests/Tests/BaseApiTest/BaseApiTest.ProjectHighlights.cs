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
        protected async Task<HighlightDto> AssertGetProjectHighlight(int personId, int companyId, int positionId, int projectId, int highlightId)
        {
            var highlight = await ProjectHighlightsController.GetHighlight(personId, companyId, positionId, projectId, highlightId);
            Assert.That(highlight, Is.Not.Null, "Failed to retrieve project highlight");
            return highlight;
        }

        protected async Task<List<HighlightDto>> AssertGetProjectHighlights(int personId, int companyId, int positionId, int projectId)
        {
            var highlights = await ProjectHighlightsController.GetHighlights(personId, companyId, positionId, projectId);
            Assert.That(highlights, Is.Not.Null, "Failed to retrieve project highlights");
            return highlights;
        }

        protected async Task<HighlightDto> AssertCreateProjectHighlight(int personId, int companyId, int positionId, int projectId, HighlightOptions options)
        {
            var response = await ProjectHighlightsController.CreateHighlight(personId, companyId, positionId, projectId, options);
            Assert.That(response.IsSuccessStatusCode(), "Project highlight creation failed");
            var highlight = response.GetObject<HighlightDto>();
            return highlight;
        }

        protected async Task<HighlightDto> AssertUpdateProjectHighlight(int personId, int companyId, int positionId, int projectId, int highlightId, HighlightOptions options)
        {
            var response = await ProjectHighlightsController.UpdateHighlight(personId, companyId, positionId, projectId, highlightId, options);
            Assert.That(response.IsSuccessStatusCode(), "Project highlight update failed");
            var highlight = response.GetObject<HighlightDto>();
            return highlight;
        }

        protected async Task<Result> AssertDeleteProjectHighlight(int personId, int companyId, int positionId, int projectId, int highlightId)
        {
            var result = await ProjectHighlightsController.DeleteHighlight(personId, companyId, positionId, projectId, highlightId);
            Assert.That(result.Succeeded, "Project highlight deletion failed");
            return result;
        }
    }
}
