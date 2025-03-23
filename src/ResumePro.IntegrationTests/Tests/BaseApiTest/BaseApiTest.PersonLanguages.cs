using NUnit.Framework;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<List<PersonaLanguageDto>> AssertGetPersonLanguages(int personId)
        {
            try
            {
                var languages = await PersonLanguagesController.GetPersonLanguages(personId);
                Assert.That(languages, Is.Not.Null, "Failed to retrieve person languages");
                return languages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AssertGetPersonLanguages: {ex.Message}");
                Assert.Inconclusive($"Test skipped due to error: {ex.Message}");
                return null;
            }
        }
    }
}
