using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/templates")]
public sealed class TemplatesController : BaseController, ITemplatesController
{
    private readonly ITemplateService _service;

    public TemplatesController(IServiceProvider serviceProvider, ITemplateService service) : base(serviceProvider)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<List<TemplateDto>> GetTemplates()
    {
        return await _service.GetTemplates<TemplateDto>(OrganizationId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<TemplateDto>> CreateTemplate([FromBody] TemplateOptions options)
    {
        var result = await _service.CreateTemplate(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{templateId}")]
    public async Task<ActionResult<TemplateDto>> UpdateTemplate([FromRoute] int templateId,
        [FromBody] TemplateOptions options)
    {
        var result = await _service.UpdateTemplate(OrganizationId, templateId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}