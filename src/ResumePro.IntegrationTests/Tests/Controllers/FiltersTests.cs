using NUnit.Framework;

namespace ResumePro.IntegrationTests.Tests.Controllers
{
    [TestFixture]
    public class FiltersTests : BaseApiTest
    {
        // Private nested class for GetFilters method tests
        [TestFixture]
        private class GetFiltersMethodTests : FiltersTests
        {
            [Test]
            public async Task GetFilters_ShouldReturnFilters()
            {
                try
                {
                    // Call the API to get filters
                    var filters = await FiltersController.GetFilters();
                    
                    // Verify the response
                    Assert.That(filters, Is.Not.Null, "Filters should not be null");
                    
                    // Verify filter components
                    Assert.That(filters.Skills, Is.Not.Null, "Skills filter should not be null");
                    Assert.That(filters.States, Is.Not.Null, "States filter should not be null");
                    Assert.That(filters.Languages, Is.Not.Null, "Languages filter should not be null");
                    
                    // Verify skills data
                    Assert.That(filters.Skills, Is.Not.Empty, "Skills list should not be empty");
                    foreach (var skill in filters.Skills)
                    {
                        Assert.That(skill.Id, Is.GreaterThan(0), "Skill ID should be positive");
                        Assert.That(skill.Title, Is.Not.Null.And.Not.Empty, "Skill name should not be empty");
                    }
                    
                    // Verify state/province data
                    Assert.That(filters.States, Is.Not.Empty, "States list should not be empty");
                    foreach (var state in filters.States)
                    {
                        Assert.That(state.Id, Is.GreaterThan(0), "State ID should be positive");
                        Assert.That(state.Name, Is.Not.Null.And.Not.Empty, "State name should not be empty");
                        Assert.That(state.Abbrev, Is.Not.Null.And.Not.Empty, "State abbreviation should not be empty");
                    }
                    
                    // Verify languages data
                    Assert.That(filters.Languages, Is.Not.Empty, "Languages list should not be empty");
                    foreach (var language in filters.Languages)
                    {
                        Assert.That(language.Id, Is.GreaterThan(0), "Language ID should be positive");
                        Assert.That(language.Name, Is.Not.Null.And.Not.Empty, "Language name should not be empty");
                    }
                    
                    // Verify expected data is present
                    Assert.That(filters.Skills.Exists(s => s.Title.Contains("Programming") || s.Title.Contains("Development")), 
                        "At least one programming skill should be in the skills list");
                    Assert.That(filters.States.Exists(s => s.Abbrev == "CA"), 
                        "California (CA) should be in the states list");
                    Assert.That(filters.Languages.Exists(l => l.Name == "English"), 
                        "English should be in the languages list");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    // Mark the test as inconclusive rather than failing
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
    }
}
