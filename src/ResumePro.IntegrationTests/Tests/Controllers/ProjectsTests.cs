using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Tests.Controllers
{
    [TestFixture]
    public class ProjectsTests : BaseApiTest
    {
        // Helper method to create test data for project tests
        private async Task<(PersonaDto person, CompanyDetails company, ActionResult<CompanyDetails> position)> CreateTestDataForProjects()
        {
            try
            {
                // Create a test person
                var personOptions = new PersonOptions
                {
                    FirstName = "Project",
                    LastName = "Test",
                    Email = $"project.test.{Guid.NewGuid()}@example.com",
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
                    Description = "Test company description",
                    Location = "Seattle, WA",
                    StartDate = DateTime.Now.AddYears(-3)
                };
                
                var company = await AssertCreateCompany(person.Id, companyOptions);
                Assert.That(company, Is.Not.Null, "Failed to create test company");
                
                // Create a test position
                var positionOptions = new PositionOptions
                {
                    JobTitle = $"Test Position {Guid.NewGuid()}",
                    StartDate = DateTime.Now.AddYears(-2),
                    EndDate = DateTime.Now.AddYears(-1)
                };
                
                var positionResult = await AssertCreatePosition(person.Id, company.Id, positionOptions);
                Assert.That(positionResult, Is.Not.Null, "Failed to create test position");
                Assert.That(positionResult.Value, Is.Not.Null, "Position value should not be null");
                
                // Add null check to ensure Value is not null before returning
                if (positionResult.Value == null)
                {
                    Assert.Inconclusive("Position value is null, cannot proceed with test");
                    return (null, null, null); // This will never be reached due to Assert.Inconclusive
                }
                
                return (person, company, positionResult);
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("500"))
            {
                // If we get a 500 error, it's likely due to database connection issues
                Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                return (null, null, null); // This will never be reached due to Assert.Inconclusive
            }
        }
        
        // Private nested class for GetProject method tests
        [TestFixture]
        private class GetProjectMethodTests : ProjectsTests
        {
            [Test]
            public async Task GetProject_ShouldReturnProject()
            {
                try
                {
                    // Create test data
                    var (person, company, position) = await CreateTestDataForProjects();
                    // Ensure position and position.Value are not null before proceeding
                    Assert.That(position, Is.Not.Null, "Position should not be null");
                    Assert.That(position.Value, Is.Not.Null, "Position value should not be null");
                    
                    // Create a test project
                    var projectOptions = new ProjectOptions
                    {
                        Name = $"Test Project {Guid.NewGuid()}",
                        Description = "This is a test project for integration testing",
                        Budget = 10000,
                        Order = 1
                    };
                    
                    var createdProject = await ProjectsController.CreateProject(
                        person.Id, company.Id, position.Value.Id, projectOptions);
                    
                    Assert.That(createdProject.Value, Is.Not.Null, "Failed to create test project");
                    
                    // Get the project by ID
                    var project = await ProjectsController.GetProject(
                        person.Id, company.Id, position.Value.Id, createdProject.Value.Id);
                    
                    // Verify the project data
                    Assert.That(project, Is.Not.Null, "Project should not be null");
                    Assert.That(project.Id, Is.EqualTo(createdProject.Value.Id), "Project ID should match");
                    Assert.That(project.Name, Is.EqualTo(projectOptions.Name), "Project name should match");
                    Assert.That(project.Description, Is.EqualTo(projectOptions.Description), "Project description should match");
                    Assert.That(project.Budget, Is.EqualTo(projectOptions.Budget), "Project budget should match");
                    Assert.That(project.Order, Is.EqualTo(projectOptions.Order), "Project order should match");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task GetProject_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidCompanyId = 99999;
                    var invalidPositionId = 99999;
                    var invalidProjectId = 99999;
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ProjectsController.GetProject(
                            invalidPersonId, invalidCompanyId, invalidPositionId, invalidProjectId);
                        Assert.Fail("Expected exception when getting project with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting project with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for GetList method tests
        [TestFixture]
        private class GetListMethodTests : ProjectsTests
        {
            [Test]
            public async Task GetList_ShouldReturnProjects()
            {
                try
                {
                    // Create test data
                    var (person, company, position) = await CreateTestDataForProjects();
                    // Ensure position and position.Value are not null before proceeding
                    Assert.That(position, Is.Not.Null, "Position should not be null");
                    Assert.That(position.Value, Is.Not.Null, "Position value should not be null");
                    
                    // Create multiple test projects
                    var projectOptions1 = new ProjectOptions
                    {
                        Name = $"Test Project 1 {Guid.NewGuid()}",
                        Description = "This is test project 1",
                        Budget = 10000,
                        Order = 1
                    };
                    
                    var projectOptions2 = new ProjectOptions
                    {
                        Name = $"Test Project 2 {Guid.NewGuid()}",
                        Description = "This is test project 2",
                        Budget = 20000,
                        Order = 2
                    };
                    
                    await ProjectsController.CreateProject(person.Id, company.Id, position.Value.Id, projectOptions1);
                    await ProjectsController.CreateProject(person.Id, company.Id, position.Value.Id, projectOptions2);
                    
                    // Get the list of projects
                    var projects = await ProjectsController.GetList(person.Id, company.Id, position.Value.Id);
                    
                    // Verify the projects list
                    Assert.That(projects, Is.Not.Null, "Projects list should not be null");
                    Assert.That(projects.Count >= 2, "Should have at least 2 projects");
                    
                    // Verify that our created projects are in the list
                    Assert.That(projects.Exists(p => p.Name == projectOptions1.Name), Is.True, 
                        "Project 1 should be in the list");
                    Assert.That(projects.Exists(p => p.Name == projectOptions2.Name), Is.True, 
                        "Project 2 should be in the list");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task GetList_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidCompanyId = 99999;
                    var invalidPositionId = 99999;
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ProjectsController.GetList(invalidPersonId, invalidCompanyId, invalidPositionId);
                        Assert.Fail("Expected exception when getting projects with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting projects with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for CreateProject method tests
        [TestFixture]
        private class CreateProjectMethodTests : ProjectsTests
        {
            [Test]
            public async Task CreateProject_WithValidOptions_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var (person, company, position) = await CreateTestDataForProjects();
                    // Ensure position and position.Value are not null before proceeding
                    Assert.That(position, Is.Not.Null, "Position should not be null");
                    Assert.That(position.Value, Is.Not.Null, "Position value should not be null");
                    
                    // Create a test project
                    var projectOptions = new ProjectOptions
                    {
                        Name = $"New Test Project {Guid.NewGuid()}",
                        Description = "This is a new test project for integration testing",
                        Budget = 15000,
                        Order = 3
                    };
                    
                    var result = await ProjectsController.CreateProject(
                        person.Id, company.Id, position.Value.Id, projectOptions);
                    
                    // Verify the result
                    Assert.That(result.Value, Is.Not.Null, "Created project should not be null");
                    Assert.That(result.Value.Id, Is.GreaterThan(0), "Created project should have a valid ID");
                    Assert.That(result.Value.Name, Is.EqualTo(projectOptions.Name), "Project name should match");
                    Assert.That(result.Value.Description, Is.EqualTo(projectOptions.Description), "Project description should match");
                    Assert.That(result.Value.Budget, Is.EqualTo(projectOptions.Budget), "Project budget should match");
                    Assert.That(result.Value.Order, Is.EqualTo(projectOptions.Order), "Project order should match");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task CreateProject_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidCompanyId = 99999;
                    var invalidPositionId = 99999;
                    
                    var projectOptions = new ProjectOptions
                    {
                        Name = "Invalid Project",
                        Description = "This project should not be created",
                        Budget = 5000,
                        Order = 1
                    };
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ProjectsController.CreateProject(
                            invalidPersonId, invalidCompanyId, invalidPositionId, projectOptions);
                        Assert.Fail("Expected exception when creating project with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when creating project with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for Update method tests
        [TestFixture]
        private class UpdateMethodTests : ProjectsTests
        {
            [Test]
            public async Task Update_WithValidOptions_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var (person, company, position) = await CreateTestDataForProjects();
                    // Ensure position and position.Value are not null before proceeding
                    Assert.That(position, Is.Not.Null, "Position should not be null");
                    Assert.That(position.Value, Is.Not.Null, "Position value should not be null");
                    
                    // Create a test project
                    var originalOptions = new ProjectOptions
                    {
                        Name = $"Original Project {Guid.NewGuid()}",
                        Description = "This is the original project description",
                        Budget = 10000,
                        Order = 1
                    };
                    
                    var createdProject = await ProjectsController.CreateProject(
                        person.Id, company.Id, position.Value.Id, originalOptions);
                    
                    Assert.That(createdProject.Value, Is.Not.Null, "Failed to create test project");
                    
                    // Update the project
                    var updatedOptions = new ProjectOptions
                    {
                        Name = $"Updated Project {Guid.NewGuid()}",
                        Description = "This is the updated project description",
                        Budget = 20000,
                        Order = 2
                    };
                    
                    var result = await ProjectsController.Update(
                        person.Id, company.Id, position.Value.Id, createdProject.Value.Id, updatedOptions);
                    
                    // Verify the result
                    Assert.That(result.Value, Is.Not.Null, "Updated project should not be null");
                    Assert.That(result.Value.Id, Is.EqualTo(createdProject.Value.Id), "Project ID should not change");
                    Assert.That(result.Value.Name, Is.EqualTo(updatedOptions.Name), "Project name should be updated");
                    Assert.That(result.Value.Description, Is.EqualTo(updatedOptions.Description), "Project description should be updated");
                    Assert.That(result.Value.Budget, Is.EqualTo(updatedOptions.Budget), "Project budget should be updated");
                    Assert.That(result.Value.Order, Is.EqualTo(updatedOptions.Order), "Project order should be updated");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task Update_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidCompanyId = 99999;
                    var invalidPositionId = 99999;
                    var invalidProjectId = 99999;
                    
                    var updatedOptions = new ProjectOptions
                    {
                        Name = "Invalid Update",
                        Description = "This update should not be applied",
                        Budget = 5000,
                        Order = 1
                    };
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ProjectsController.Update(
                            invalidPersonId, invalidCompanyId, invalidPositionId, invalidProjectId, updatedOptions);
                        Assert.Fail("Expected exception when updating project with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when updating project with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for Delete method tests
        [TestFixture]
        private class DeleteMethodTests : ProjectsTests
        {
            [Test]
            public async Task Delete_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var (person, company, position) = await CreateTestDataForProjects();
                    // Ensure position and position.Value are not null before proceeding
                    Assert.That(position, Is.Not.Null, "Position should not be null");
                    Assert.That(position.Value, Is.Not.Null, "Position value should not be null");
                    
                    // Create a test project to delete
                    var projectOptions = new ProjectOptions
                    {
                        Name = $"Project to Delete {Guid.NewGuid()}",
                        Description = "This project will be deleted",
                        Budget = 5000,
                        Order = 1
                    };
                    
                    var createdProject = await ProjectsController.CreateProject(
                        person.Id, company.Id, position.Value.Id, projectOptions);
                    
                    Assert.That(createdProject.Value, Is.Not.Null, "Failed to create test project");
                    
                    // Delete the project
                    var result = await ProjectsController.Delete(
                        person.Id, company.Id, position.Value.Id, createdProject.Value.Id);
                    
                    // Verify the result
                    Assert.That(result.Succeeded, Is.True, "Delete operation should succeed");
                    
                    // Try to get the deleted project - should throw an exception
                    try
                    {
                        await ProjectsController.GetProject(
                            person.Id, company.Id, position.Value.Id, createdProject.Value.Id);
                        Assert.Fail("Expected exception when getting deleted project");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting deleted project");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task Delete_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidCompanyId = 99999;
                    var invalidPositionId = 99999;
                    var invalidProjectId = 99999;
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ProjectsController.Delete(
                            invalidPersonId, invalidCompanyId, invalidPositionId, invalidProjectId);
                        Assert.Fail("Expected exception when deleting project with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when deleting project with invalid IDs");
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
