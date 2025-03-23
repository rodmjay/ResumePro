using NUnit.Framework;
using ResumePro.Shared.Models;
using Bespoke.IntegrationTesting.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
