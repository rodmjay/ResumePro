﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;

namespace ResumePro.Shared.Interfaces;

public interface ITemplatesController
{
    Task<List<TemplateDto>> GetTemplates();
    Task<ActionResult<TemplateDto>> CreateTemplate(TemplateOptions options);
    Task<ActionResult<TemplateDto>> UpdateTemplate(int templateId, TemplateOptions options);
}