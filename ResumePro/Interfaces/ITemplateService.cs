#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Interfaces;

public interface ITemplateService : IService<Template>
{
    Task<List<T>> GetTemplates<T>(int organizationId) where T : TemplateDto;
    Task<T> GetTemplate<T>(int organizationId, string templateId) where T : TemplateDto;
    Task<OneOf<TemplateDto, Result>> CreateTemplate(int organizationId, TemplateOptions options);
    Task<OneOf<TemplateDto, Result>> UpdateTemplate(int organizationId, int templateId, TemplateOptions options);
}