#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies;

public sealed class TemplatesProxy(HttpClient httpClient) : BaseProxy(httpClient), ITemplatesController
{
    public async Task<List<TemplateDto>> GetTemplates()
    {
        return await DoGet<List<TemplateDto>>("v1.0/templates")
            .ConfigureAwait(true);
    }

    public async Task<ActionResult<TemplateDto>> CreateTemplate(TemplateOptions options)
    {
        return await DoPostActionResult<TemplateOptions, TemplateDto>("v1.0/templates", options)
            .ConfigureAwait(true);
    }

    public async Task<ActionResult<TemplateDto>> UpdateTemplate(int templateId, TemplateOptions options)
    {
        return await DoPutActionResult<TemplateOptions, TemplateDto>($"v1.0/templates/{templateId}", options)
            .ConfigureAwait(true);
    }
}