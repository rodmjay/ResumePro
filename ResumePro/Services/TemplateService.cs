﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class TemplateService(IServiceProvider serviceProvider)
    : BaseService<Template>(serviceProvider), ITemplateService
{
    private IQueryable<Template> Templates => Repository.Queryable();

    public Task<List<T>> GetTemplates<T>(int organizationId) where T : TemplateDto
    {
        return Templates.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId || x.OrganizationId == null)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetTemplate<T>(int organizationId, string templateId) where T : TemplateDto
    {
        return Templates.AsNoTracking()
            .Where(x => (x.OrganizationId == organizationId || x.OrganizationId == null) && x.Name == templateId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<TemplateDto, Result>> CreateTemplate(int organizationId, TemplateOptions options)
    {
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, Options: {@options}"),
            organizationId, options);

        var template = new Template
        {
            //Id = await GetNextTemplateId(),
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            Name = options.Name,
            Engine = options.Engine,
            Source = options.Template,
            Format = options.Format
        };

        var records = Repository.Insert(template, true);
        if (records > 0) return await GetTemplate<TemplateDto>(organizationId, template.Name);

        return Result.Failed();
    }

    public async Task<OneOf<TemplateDto, Result>> UpdateTemplate(int organizationId, int templateId,
        TemplateOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, TemplateId: {templateId}, Options: {@options}"),
            organizationId, templateId, options);

        var template = await Templates.Where(x => x.OrganizationId == organizationId && x.Id == templateId)
            .FirstOrDefaultAsync();

        template.ObjectState = ObjectState.Modified;
        template.Name = options.Name;
        template.Format = options.Format;
        template.Engine = options.Engine;
        template.Source = options.Template;

        var records = Repository.InsertOrUpdateGraph(template, true);
        if (records > 0) return await GetTemplate<TemplateDto>(organizationId, template.Name);

        return Result.Failed();
    }

    private async Task<int> GetNextTemplateId()
    {
        var id = await Templates.AsNoTracking()
            .IgnoreQueryFilters()
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}