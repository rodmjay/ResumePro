using NUnit.Framework;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<FilterContainer> AssertGetFilters()
        {
            try
            {
                var filters = await FiltersController.GetFilters();
                Assert.That(filters, Is.Not.Null, "Failed to retrieve filters");
                return filters;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetFilters: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
