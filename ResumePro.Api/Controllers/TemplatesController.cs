#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/templates")]
public sealed class TemplatesController(IServiceProvider serviceProvider, ITemplateService service)
    : BaseController(serviceProvider), ITemplatesController
{

    [HttpGet]
    public async Task<List<TemplateDto>> GetTemplates()
    {
        return await service.GetTemplates<TemplateDto>(OrganizationId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<TemplateDto>> CreateTemplate([FromBody] TemplateOptions options)
    {
        var result = await service.CreateTemplate(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpPut("{templateId}")]
    public async Task<ActionResult<TemplateDto>> UpdateTemplate([FromRoute] string templateId, [FromBody] TemplateOptions options)
    {
        var result = await service.UpdateTemplate(OrganizationId, templateId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

}