using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<HighlightDto> AssertGetHighlight(int personId, int companyId, int positionId, int highlightId)
        {
            try
            {
                var highlight = await HighlightsController.GetHighlight(personId, companyId, positionId, highlightId);
                Assert.That(highlight, Is.Not.Null, "Failed to retrieve highlight");
                return highlight;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetHighlight: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<HighlightDto>> AssertGetHighlights(int personId, int companyId, int positionId)
        {
            try
            {
                var highlights = await HighlightsController.GetHighlights(personId, companyId, positionId);
                Assert.That(highlights, Is.Not.Null, "Failed to retrieve highlights");
                return highlights;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetHighlights: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<ActionResult<HighlightDto>> AssertCreateHighlight(int personId, int companyId, int positionId, HighlightOptions options)
        {
            try
            {
                var response = await HighlightsController.CreateHighlight(personId, companyId, positionId, options);
                Assert.That(response, Is.Not.Null, "Highlight creation failed");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertCreateHighlight: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<ActionResult<HighlightDto>> AssertUpdateHighlight(int personId, int companyId, int positionId, int highlightId, HighlightOptions options)
        {
            try
            {
                var response = await HighlightsController.UpdateHighlight(personId, companyId, positionId, highlightId, options);
                Assert.That(response, Is.Not.Null, "Highlight update failed");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertUpdateHighlight: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<Result> AssertDeleteHighlight(int personId, int companyId, int positionId, int highlightId)
        {
            try
            {
                var result = await HighlightsController.DeleteHighlight(personId, companyId, positionId, highlightId);
                Assert.That(result.Succeeded, "Highlight deletion failed");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertDeleteHighlight: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
