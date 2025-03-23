using Bespoke.IntegrationTesting.Bases;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class TemplatesProxy : BaseProxy, ITemplatesController
{
    private const string RootUrl = "v1.0/templates";

    public TemplatesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<TemplateDto>> GetTemplates()
    {
        return await DoGetAsync<List<TemplateDto>>(RootUrl);
    }

    public async Task<ActionResult<TemplateDto>> CreateTemplate(TemplateOptions options)
    {
        return await DoPostActionResultAsync<TemplateOptions, TemplateDto>(RootUrl, options);
    }

    public async Task<ActionResult<TemplateDto>> UpdateTemplate(int templateId, TemplateOptions options)
    {
        return await DoPutActionResultAsync<TemplateOptions, TemplateDto>($"{RootUrl}/{templateId}", options);
    }
}
