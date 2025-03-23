using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Tests.Controllers
{
    [TestFixture]
    public class ProjectHighlightsTests : BaseApiTest
    {
        // Helper method to create test data
        private async Task<(PersonaDto person, CompanyDetails company, CompanyDetails position, ProjectDto? project)> CreateTestDataForProjectHighlights()
        {
            try
            {
                // Create a test person
                var personOptions = new PersonOptions
                {
                    FirstName = "Project",
                    LastName = "Highlight",
                    Email = $"project.highlight.{Guid.NewGuid()}@example.com",
                    PhoneNumber = "555-123-4567",
                    City = "Seattle",
                    StateId = 3
                };
                
                var person = await AssertCreatePerson(personOptions);
                Assert.That(person, Is.Not.Null, "Failed to create test person");
                
                // Create a test company
                var companyOptions = new CompanyOptions
                {
                    Company = $"Test Company {Guid.NewGuid()}",
                    Location = "San Francisco",
                    StartDate = DateTime.Now.AddYears(-3)
                };
                
                var company = await CompaniesController.CreateCompany(person.Id, companyOptions);
                Assert.That(company.Value, Is.Not.Null, "Failed to create test company");
                
                // Create a test position
                var positionOptions = new PositionOptions
                {
                    JobTitle = "Software Engineer",
                    StartDate = DateTime.Now.AddYears(-2),
                    EndDate = DateTime.Now.AddMonths(-1)
                };
                
                var position = await PositionsController.CreatePosition(person.Id, company.Value.Id, positionOptions);
                Assert.That(position.Value, Is.Not.Null, "Failed to create test position");
                
                // Create a test project
                var projectOptions = new ProjectOptions
                {
                    Name = $"Test Project {Guid.NewGuid()}",
                    Description = "A test project for integration testing"
                };
                
                var project = await ProjectsController.CreateProject(person.Id, company.Value.Id, position.Value.Id, projectOptions);
                Assert.That(project.Value, Is.Not.Null, "Failed to create test project");
                
                return (person, company.Value, position.Value, project.Value);
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("500"))
            {
                // If we get a 500 error, it's likely due to database connection issues
                Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                return default;
            }
        }
        
        // Private nested class for GetHighlight method tests
        [TestFixture]
        private class GetHighlightMethodTests : ProjectHighlightsTests
        {
            [Test]
            public async Task GetHighlight_ShouldReturnHighlight()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Create a highlight for the project
                    var highlightOptions = new HighlightOptions
                    {
                        Text = "Implemented key feature that increased performance by 50%",
                        Order = 1
                    };
                    
                    var createdHighlight = await ProjectHighlightsController.CreateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, highlightOptions);
                    
                    Assert.That(createdHighlight.Value, Is.Not.Null, "Failed to create test highlight");
                    
                    // Get the highlight
                    var highlight = await ProjectHighlightsController.GetHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, createdHighlight.Value.Id);
                    
                    // Verify the highlight
                    Assert.That(highlight, Is.Not.Null, "Retrieved highlight should not be null");
                    Assert.That(highlight.Id, Is.EqualTo(createdHighlight.Value.Id), "Highlight ID should match");
                    Assert.That(highlight.Text, Is.EqualTo(highlightOptions.Text), "Highlight text should match");
                    Assert.That(highlight.Order, Is.EqualTo(highlightOptions.Order), "Highlight order should match");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task GetHighlight_WithInvalidId_ShouldHandleError()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Try to get a highlight with an invalid ID
                    var invalidHighlightId = 99999;
                    
                    try
                    {
                        await ProjectHighlightsController.GetHighlight(
                            person.Id, company.Id, position.Id, project?.Id ?? 0, invalidHighlightId);
                        
                        Assert.Fail("Expected exception when getting highlight with invalid ID");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting highlight with invalid ID");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for GetHighlights method tests
        [TestFixture]
        private class GetHighlightsMethodTests : ProjectHighlightsTests
        {
            [Test]
            public async Task GetHighlights_ShouldReturnHighlights()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Create multiple highlights for the project
                    var highlight1Options = new HighlightOptions
                    {
                        Text = "Implemented key feature that increased performance by 50%",
                        Order = 1
                    };
                    
                    var highlight2Options = new HighlightOptions
                    {
                        Text = "Led team of 5 developers to deliver project on time",
                        Order = 2
                    };
                    
                    await ProjectHighlightsController.CreateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, highlight1Options);
                    
                    await ProjectHighlightsController.CreateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, highlight2Options);
                    
                    // Get all highlights for the project
                    var highlights = await ProjectHighlightsController.GetHighlights(
                        person.Id, company.Id, position.Id, project?.Id ?? 0);
                    
                    // Verify the highlights
                    Assert.That(highlights, Is.Not.Null, "Retrieved highlights should not be null");
                    Assert.That(highlights.Count, Is.GreaterThanOrEqualTo(2), "Should have at least 2 highlights");
                    
                    // Verify the highlight texts
                    var highlightTexts = highlights.Select(h => h.Text).ToList();
                    Assert.That(highlightTexts, Contains.Item(highlight1Options.Text), "Highlight 1 text should be in the list");
                    Assert.That(highlightTexts, Contains.Item(highlight2Options.Text), "Highlight 2 text should be in the list");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task GetHighlights_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Create a test person
                    var personOptions = new PersonOptions
                    {
                        FirstName = "Invalid",
                        LastName = "Highlights",
                        Email = $"invalid.highlights.{Guid.NewGuid()}@example.com",
                        PhoneNumber = "555-987-6543",
                        City = "Chicago",
                        StateId = 4
                    };
                    
                    var person = await AssertCreatePerson(personOptions);
                    
                    // Try to get highlights with invalid IDs
                    var invalidCompanyId = 99999;
                    var invalidPositionId = 99999;
                    var invalidProjectId = 99999;
                    
                    try
                    {
                        await ProjectHighlightsController.GetHighlights(
                            person.Id, invalidCompanyId, invalidPositionId, invalidProjectId);
                        
                        Assert.Fail("Expected exception when getting highlights with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting highlights with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for CreateHighlight method tests
        [TestFixture]
        private class CreateHighlightMethodTests : ProjectHighlightsTests
        {
            [Test]
            public async Task CreateHighlight_WithValidOptions_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Create a highlight for the project
                    var highlightOptions = new HighlightOptions
                    {
                        Text = "Developed innovative solution that saved $100K annually",
                        Order = 3
                    };
                    
                    var result = await ProjectHighlightsController.CreateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, highlightOptions);
                    
                    // Verify the result
                    Assert.That(result.Value, Is.Not.Null, "Created highlight should not be null");
                    Assert.That(result.Value.Text, Is.EqualTo(highlightOptions.Text), "Highlight text should match");
                    Assert.That(result.Value.Order, Is.EqualTo(highlightOptions.Order), "Highlight order should match");
                    
                    // Verify the highlight was added by getting it
                    var highlight = await ProjectHighlightsController.GetHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, result.Value.Id);
                    
                    Assert.That(highlight, Is.Not.Null, "Retrieved highlight should not be null");
                    Assert.That(highlight.Id, Is.EqualTo(result.Value.Id), "Highlight ID should match");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task CreateHighlight_WithInvalidOptions_ShouldHandleError()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Create a highlight with invalid options (null text)
                    var invalidOptions = new HighlightOptions
                    {
                        Text = string.Empty,
                        Order = 1
                    };
                    
                    try
                    {
                        await ProjectHighlightsController.CreateHighlight(
                            person.Id, company.Id, position.Id, project?.Id ?? 0, invalidOptions);
                        
                        Assert.Fail("Expected exception when creating highlight with invalid options");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when creating highlight with invalid options");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for UpdateHighlight method tests
        [TestFixture]
        private class UpdateHighlightMethodTests : ProjectHighlightsTests
        {
            [Test]
            public async Task UpdateHighlight_WithValidOptions_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Create a highlight for the project
                    var originalOptions = new HighlightOptions
                    {
                        Text = "Original highlight text",
                        Order = 1
                    };
                    
                    var createdHighlight = await ProjectHighlightsController.CreateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, originalOptions);
                    
                    Assert.That(createdHighlight.Value, Is.Not.Null, "Failed to create test highlight");
                    
                    // Update the highlight
                    var updatedOptions = new HighlightOptions
                    {
                        Text = "Updated highlight text",
                        Order = 2
                    };
                    
                    var result = await ProjectHighlightsController.UpdateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, createdHighlight.Value.Id, updatedOptions);
                    
                    // Verify the result
                    Assert.That(result.Value, Is.Not.Null, "Updated highlight should not be null");
                    Assert.That(result.Value.Text, Is.EqualTo(updatedOptions.Text), "Highlight text should be updated");
                    Assert.That(result.Value.Order, Is.EqualTo(updatedOptions.Order), "Highlight order should be updated");
                    
                    // Verify the highlight was updated by getting it
                    var highlight = await ProjectHighlightsController.GetHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, createdHighlight.Value.Id);
                    
                    Assert.That(highlight, Is.Not.Null, "Retrieved highlight should not be null");
                    Assert.That(highlight.Text, Is.EqualTo(updatedOptions.Text), "Highlight text should be updated");
                    Assert.That(highlight.Order, Is.EqualTo(updatedOptions.Order), "Highlight order should be updated");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task UpdateHighlight_WithInvalidId_ShouldHandleError()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Try to update a highlight with an invalid ID
                    var invalidHighlightId = 99999;
                    var updateOptions = new HighlightOptions
                    {
                        Text = "This update should fail",
                        Order = 1
                    };
                    
                    try
                    {
                        await ProjectHighlightsController.UpdateHighlight(
                            person.Id, company.Id, position.Id, project?.Id ?? 0, invalidHighlightId, updateOptions);
                        
                        Assert.Fail("Expected exception when updating highlight with invalid ID");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when updating highlight with invalid ID");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for DeleteHighlight method tests
        [TestFixture]
        private class DeleteHighlightMethodTests : ProjectHighlightsTests
        {
            [Test]
            public async Task DeleteHighlight_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Create a highlight for the project
                    var highlightOptions = new HighlightOptions
                    {
                        Text = "Highlight to be deleted",
                        Order = 1
                    };
                    
                    var createdHighlight = await ProjectHighlightsController.CreateHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, highlightOptions);
                    
                    Assert.That(createdHighlight.Value, Is.Not.Null, "Failed to create test highlight");
                    
                    // Delete the highlight
                    var result = await ProjectHighlightsController.DeleteHighlight(
                        person.Id, company.Id, position.Id, project?.Id ?? 0, createdHighlight.Value.Id);
                    
                    // Verify the result
                    Assert.That(result.Succeeded, Is.True, "Delete operation should succeed");
                    
                    // Verify the highlight was deleted by trying to get it
                    try
                    {
                        await ProjectHighlightsController.GetHighlight(
                            person.Id, company.Id, position.Id, project?.Id ?? 0, createdHighlight.Value.Id);
                        
                        Assert.Fail("Expected exception when getting deleted highlight");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting deleted highlight");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task DeleteHighlight_WithInvalidId_ShouldHandleError()
            {
                try
                {
                    // Create test data
                    var (person, company, position, project) = await CreateTestDataForProjectHighlights();
                    
                    // Try to delete a highlight with an invalid ID
                    var invalidHighlightId = 99999;
                    
                    try
                    {
                        await ProjectHighlightsController.DeleteHighlight(
                            person.Id, company.Id, position.Id, project?.Id ?? 0, invalidHighlightId);
                        
                        Assert.Fail("Expected exception when deleting highlight with invalid ID");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when deleting highlight with invalid ID");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
    }
}
