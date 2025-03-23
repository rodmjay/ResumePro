namespace ResumePro.Api.Interfaces;

public interface ITemplatesController
{
    Task<List<TemplateDto>> GetTemplates();
    Task<ActionResult<TemplateDto>> CreateTemplate(TemplateOptions options);
    Task<ActionResult<TemplateDto>> UpdateTemplate(int templateId, TemplateOptions options);
}