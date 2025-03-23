using Bespoke.IntegrationTesting.Bases;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class OrganizationSettingsProxy : BaseProxy, IOrganizationSettingsController
{
    private const string RootUrl = "v1.0/settings";

    public OrganizationSettingsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ActionResult<OrganizationSettingsDto>> CreateSettings(OrganizationSettingsOptions options)
    {
        return await DoPostActionResultAsync<OrganizationSettingsOptions, OrganizationSettingsDto>(RootUrl, options);
    }

    public async Task<ActionResult<OrganizationSettingsDto>> UpdateSettings(OrganizationSettingsOptions options)
    {
        return await DoPutActionResultAsync<OrganizationSettingsOptions, OrganizationSettingsDto>(RootUrl, options);
    }
}
