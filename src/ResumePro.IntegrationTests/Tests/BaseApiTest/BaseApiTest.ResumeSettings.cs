using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<ResumeSettingsDto> AssertUpdateResumeSettings(int personId, int resumeId, ResumeSettingsOptions options)
        {
            var response = await ResumeSettingsController.UpdateSettings(personId, resumeId, options);
            Assert.That(response.IsSuccessStatusCode(), "Resume settings update failed");
            var settings = response.GetObject<ResumeSettingsDto>();
            return settings;
        }
    }
}
