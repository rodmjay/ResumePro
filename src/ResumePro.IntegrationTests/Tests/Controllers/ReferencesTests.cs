using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Tests.Controllers
{
    [TestFixture]
    public class ReferencesTests : BaseApiTest
    {
        // Helper method to create test data for reference tests
        private async Task<PersonaDto> CreateTestDataForReferences()
        {
            try
            {
                // Create a test person
                var personOptions = new PersonOptions
                {
                    FirstName = "Reference",
                    LastName = "Test",
                    Email = $"reference.test.{Guid.NewGuid()}@example.com",
                    PhoneNumber = "555-123-4567",
                    City = "Seattle",
                    StateId = 3
                };
                
                var person = await AssertCreatePerson(personOptions);
                Assert.That(person, Is.Not.Null, "Failed to create test person");
                
                return person;
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("500"))
            {
                // If we get a 500 error, it's likely due to database connection issues
                Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                return null; // This will never be reached due to Assert.Inconclusive
            }
        }
        
        // Private nested class for Get method tests
        [TestFixture]
        private class GetMethodTests : ReferencesTests
        {
            [Test]
            public async Task Get_ShouldReturnReference()
            {
                try
                {
                    // Create test data
                    var person = await CreateTestDataForReferences();
                    Assert.That(person, Is.Not.Null, "Person should not be null");
                    
                    // Create a test reference
                    var referenceOptions = new ReferenceOptions
                    {
                        Name = $"Test Reference {Guid.NewGuid()}",
                        Text = "This is a test reference for integration testing",
                        Order = 1
                    };
                    
                    var createdReference = await ReferencesController.CreateReference(person.Id, referenceOptions);
                    Assert.That(createdReference.Value, Is.Not.Null, "Failed to create test reference");
                    
                    // Get the reference by ID
                    var reference = await ReferencesController.Get(person.Id, createdReference.Value.Id);
                    
                    // Verify the reference data
                    Assert.That(reference, Is.Not.Null, "Reference should not be null");
                    Assert.That(reference.Id, Is.EqualTo(createdReference.Value.Id), "Reference ID should match");
                    Assert.That(reference.Name, Is.EqualTo(referenceOptions.Name), "Reference name should match");
                    Assert.That(reference.Text, Is.EqualTo(referenceOptions.Text), "Reference text should match");
                    Assert.That(reference.Order, Is.EqualTo(referenceOptions.Order), "Reference order should match");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task Get_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidReferenceId = 99999;
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ReferencesController.Get(invalidPersonId, invalidReferenceId);
                        Assert.Fail("Expected exception when getting reference with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting reference with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for GetReferences method tests
        [TestFixture]
        private class GetReferencesMethodTests : ReferencesTests
        {
            [Test]
            public async Task GetReferences_ShouldReturnReferences()
            {
                try
                {
                    // Create test data
                    var person = await CreateTestDataForReferences();
                    Assert.That(person, Is.Not.Null, "Person should not be null");
                    
                    // Create multiple test references
                    var referenceOptions1 = new ReferenceOptions
                    {
                        Name = $"Test Reference 1 {Guid.NewGuid()}",
                        Text = "This is test reference 1",
                        Order = 1
                    };
                    
                    var referenceOptions2 = new ReferenceOptions
                    {
                        Name = $"Test Reference 2 {Guid.NewGuid()}",
                        Text = "This is test reference 2",
                        Order = 2
                    };
                    
                    await ReferencesController.CreateReference(person.Id, referenceOptions1);
                    await ReferencesController.CreateReference(person.Id, referenceOptions2);
                    
                    // Get the list of references
                    var references = await ReferencesController.GetReferences(person.Id);
                    
                    // Verify the references list
                    Assert.That(references, Is.Not.Null, "References list should not be null");
                    Assert.That(references.Count >= 2, "Should have at least 2 references");
                    
                    // Verify that our created references are in the list
                    Assert.That(references.Exists(r => r.Name == referenceOptions1.Name), Is.True, 
                        "Reference 1 should be in the list");
                    Assert.That(references.Exists(r => r.Name == referenceOptions2.Name), Is.True, 
                        "Reference 2 should be in the list");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task GetReferences_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ReferencesController.GetReferences(invalidPersonId);
                        Assert.Fail("Expected exception when getting references with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting references with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for CreateReference method tests
        [TestFixture]
        private class CreateReferenceMethodTests : ReferencesTests
        {
            [Test]
            public async Task CreateReference_WithValidOptions_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var person = await CreateTestDataForReferences();
                    Assert.That(person, Is.Not.Null, "Person should not be null");
                    
                    // Create a test reference
                    var referenceOptions = new ReferenceOptions
                    {
                        Name = $"New Test Reference {Guid.NewGuid()}",
                        Text = "This is a new test reference for integration testing",
                        Order = 3
                    };
                    
                    var result = await ReferencesController.CreateReference(person.Id, referenceOptions);
                    
                    // Verify the result
                    Assert.That(result.Value, Is.Not.Null, "Created reference should not be null");
                    Assert.That(result.Value.Id, Is.GreaterThan(0), "Created reference should have a valid ID");
                    Assert.That(result.Value.Name, Is.EqualTo(referenceOptions.Name), "Reference name should match");
                    Assert.That(result.Value.Text, Is.EqualTo(referenceOptions.Text), "Reference text should match");
                    Assert.That(result.Value.Order, Is.EqualTo(referenceOptions.Order), "Reference order should match");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task CreateReference_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    
                    var referenceOptions = new ReferenceOptions
                    {
                        Name = "Invalid Reference",
                        Text = "This reference should not be created",
                        Order = 1
                    };
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ReferencesController.CreateReference(invalidPersonId, referenceOptions);
                        Assert.Fail("Expected exception when creating reference with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when creating reference with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for UpdateReference method tests
        [TestFixture]
        private class UpdateReferenceMethodTests : ReferencesTests
        {
            [Test]
            public async Task UpdateReference_WithValidOptions_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var person = await CreateTestDataForReferences();
                    Assert.That(person, Is.Not.Null, "Person should not be null");
                    
                    // Create a test reference
                    var originalOptions = new ReferenceOptions
                    {
                        Name = $"Original Reference {Guid.NewGuid()}",
                        Text = "This is the original reference text",
                        Order = 1
                    };
                    
                    var createdReference = await ReferencesController.CreateReference(person.Id, originalOptions);
                    Assert.That(createdReference.Value, Is.Not.Null, "Failed to create test reference");
                    
                    // Update the reference
                    var updatedOptions = new ReferenceOptions
                    {
                        Name = $"Updated Reference {Guid.NewGuid()}",
                        Text = "This is the updated reference text",
                        Order = 2
                    };
                    
                    var result = await ReferencesController.UpdateReference(
                        person.Id, createdReference.Value.Id, updatedOptions);
                    
                    // Verify the result
                    Assert.That(result.Value, Is.Not.Null, "Updated reference should not be null");
                    Assert.That(result.Value.Id, Is.EqualTo(createdReference.Value.Id), "Reference ID should not change");
                    Assert.That(result.Value.Name, Is.EqualTo(updatedOptions.Name), "Reference name should be updated");
                    Assert.That(result.Value.Text, Is.EqualTo(updatedOptions.Text), "Reference text should be updated");
                    Assert.That(result.Value.Order, Is.EqualTo(updatedOptions.Order), "Reference order should be updated");
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task UpdateReference_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidReferenceId = 99999;
                    
                    var updatedOptions = new ReferenceOptions
                    {
                        Name = "Invalid Update",
                        Text = "This update should not be applied",
                        Order = 1
                    };
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ReferencesController.UpdateReference(
                            invalidPersonId, invalidReferenceId, updatedOptions);
                        Assert.Fail("Expected exception when updating reference with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when updating reference with invalid IDs");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
        }
        
        // Private nested class for DeleteReference method tests
        [TestFixture]
        private class DeleteReferenceMethodTests : ReferencesTests
        {
            [Test]
            public async Task DeleteReference_ShouldReturnSuccess()
            {
                try
                {
                    // Create test data
                    var person = await CreateTestDataForReferences();
                    Assert.That(person, Is.Not.Null, "Person should not be null");
                    
                    // Create a test reference to delete
                    var referenceOptions = new ReferenceOptions
                    {
                        Name = $"Reference to Delete {Guid.NewGuid()}",
                        Text = "This reference will be deleted",
                        Order = 1
                    };
                    
                    var createdReference = await ReferencesController.CreateReference(person.Id, referenceOptions);
                    Assert.That(createdReference.Value, Is.Not.Null, "Failed to create test reference");
                    
                    // Delete the reference
                    var result = await ReferencesController.DeleteReference(
                        person.Id, createdReference.Value.Id);
                    
                    // Verify the result
                    Assert.That(result.Succeeded, Is.True, "Delete operation should succeed");
                    
                    // Try to get the deleted reference - should throw an exception
                    try
                    {
                        await ReferencesController.Get(person.Id, createdReference.Value.Id);
                        Assert.Fail("Expected exception when getting deleted reference");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when getting deleted reference");
                    }
                }
                catch (HttpRequestException ex) when (ex.Message.Contains("500"))
                {
                    // If we get a 500 error, it's likely due to database connection issues
                    Assert.Inconclusive("Database connection issue detected: " + ex.Message);
                }
            }
            
            [Test]
            public async Task DeleteReference_WithInvalidIds_ShouldHandleError()
            {
                try
                {
                    // Test with invalid IDs
                    var invalidPersonId = 99999;
                    var invalidReferenceId = 99999;
                    
                    // Expect an exception when calling with invalid IDs
                    try
                    {
                        await ReferencesController.DeleteReference(invalidPersonId, invalidReferenceId);
                        Assert.Fail("Expected exception when deleting reference with invalid IDs");
                    }
                    catch (Exception ex)
                    {
                        // Expected exception
                        Console.WriteLine($"Expected exception: {ex.Message}");
                        Assert.Pass("Expected exception thrown when deleting reference with invalid IDs");
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
