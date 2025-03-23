using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<OrganizationSettingsDto> AssertCreateOrganizationSettings(OrganizationSettingsOptions options)
        {
            var response = await OrganizationSettingsController.CreateSettings(options);
            Assert.That(response.IsSuccessStatusCode(), "Organization settings creation failed");
            var settings = response.GetObject<OrganizationSettingsDto>();
            return settings;
        }

        protected async Task<OrganizationSettingsDto> AssertUpdateOrganizationSettings(OrganizationSettingsOptions options)
        {
            var response = await OrganizationSettingsController.UpdateSettings(options);
            Assert.That(response.IsSuccessStatusCode(), "Organization settings update failed");
            var settings = response.GetObject<OrganizationSettingsDto>();
            return settings;
        }
    }
}
