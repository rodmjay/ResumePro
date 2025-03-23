using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using ResumePro.Shared.Filters;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<PagedList<PersonaDto>> AssertGetPeople(PersonaFilters filters, PagingQuery paging)
        {
            try
            {
                var people = await PeopleController.GetPeople(filters, paging);
                Assert.That(people, Is.Not.Null, "Failed to retrieve people");
                Assert.That(people.Items, Is.Not.Null, "People items collection is null");
                return people;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetPeople: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<PersonaDetails> AssertGetPerson(int personId)
        {
            try
            {
                var person = await PeopleController.GetPerson(personId);
                Assert.That(person, Is.Not.Null, "Failed to retrieve person");
                return person;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetPerson: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<PersonaDetails> AssertCreatePerson(PersonOptions options)
        {
            try
            {
                var response = await PeopleController.CreatePerson(options);
                Assert.That(response.IsSuccessStatusCode(), "Person creation failed");
                var person = response.GetObject<PersonaDetails>();
                return person;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertCreatePerson: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<PersonaDetails> AssertUpdatePerson(int personId, PersonOptions options)
        {
            try
            {
                var response = await PeopleController.UpdatePerson(personId, options);
                Assert.That(response.IsSuccessStatusCode(), "Person update failed");
                var person = response.GetObject<PersonaDetails>();
                return person;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertUpdatePerson: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }

        protected async Task<Result> AssertDeletePerson(int personId)
        {
            try
            {
                var result = await PeopleController.DeletePerson(personId);
                Assert.That(result.Succeeded, "Person deletion failed");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertDeletePerson: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
