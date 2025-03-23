using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<List<TemplateDto>> AssertGetTemplates()
        {
            var templates = await TemplatesController.GetTemplates();
            Assert.That(templates, Is.Not.Null, "Failed to retrieve templates");
            return templates;
        }

        protected async Task<TemplateDto> AssertCreateTemplate(TemplateOptions options)
        {
            var response = await TemplatesController.CreateTemplate(options);
            Assert.That(response.IsSuccessStatusCode(), "Template creation failed");
            var template = response.GetObject<TemplateDto>();
            return template;
        }

        protected async Task<TemplateDto> AssertUpdateTemplate(int templateId, TemplateOptions options)
        {
            var response = await TemplatesController.UpdateTemplate(templateId, options);
            Assert.That(response.IsSuccessStatusCode(), "Template update failed");
            var template = response.GetObject<TemplateDto>();
            return template;
        }
    }
}
