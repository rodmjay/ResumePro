﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class ReferenceService(IServiceProvider serviceProvider, ReferenceErrorDescriber referenceErrors)
    : BaseService<Reference>(serviceProvider), IReferenceService
{
    private IQueryable<Reference> References => Repository.Queryable();

    public Task<List<T>> GetReferences<T>(int organizationId, int personId) where T : ReferenceDto
    {
        return References.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<T> GetReference<T>(int organizationId, int personId, int referenceId) where T : ReferenceDto
    {
        return References.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Id == referenceId)
            .OrderBy(x => x.Order)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<ReferenceDto, Result>> CreateReference(int organizationId, int personId,
        ReferenceOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId, personId, options);

        var lastReferenceOrder = await
            References.Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
                .AsNoTracking()
                .OrderByDescending(x => x.Order)
                .Select(x => x.Order)
                .FirstOrDefaultAsync();

        var reference = new Reference
        {
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            PersonId = personId,
            Text = options.Text,
            Name = options.Name,
            Id = await GetNextReferenceId(organizationId, personId),
            Order = lastReferenceOrder + 1
        };

        var results = Repository.InsertOrUpdateGraph(reference, true);
        if (results > 0)
            return await GetReference<ReferenceDto>(organizationId, personId, reference.Id);

        return Result.Failed(referenceErrors.UnableToSaveReference());
    }

    public async Task<OneOf<ReferenceDto, Result>> UpdateReference(int organizationId, int personId, int referenceId,
        ReferenceOptions options)
    {
        Logger.LogInformation(
            GetLogMessage(
                "OrganizationId: {@organizationId}, PersonId: {@personId}, ReferenceId: {@referenceId}, Options: {@options}"),
            organizationId, personId, referenceId, options);

        var reference = await References.Where(x =>
                x.OrganizationId == organizationId && x.PersonId == personId && x.Id == referenceId)
            .FirstOrDefaultAsync();

        if (reference == null)
            return Result.Failed(referenceErrors.ReferenceNotFound(referenceId));

        var references = await References
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ToListAsync();

        references.Remove(reference);

        reference.Text = options.Text;
        reference.Name = options.Name;

        var index = options.Order - 1;

        if (index < 0) index = 0;
        if (index > references.Count) index = references.Count;
        references.Insert(index, reference);

        for (var i = 0; i < references.Count; i++)
        {
            references[i].Order = i + 1;
            references[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(references[0]);
        }

        var results = Repository.Commit();
        if (results > 0)
            return await GetReference<ReferenceDto>(organizationId, personId, reference.Id);

        return Result.Failed(referenceErrors.UnableToSaveReference());
    }

    public async Task<Result> DeleteReference(int organizationId, int personId, int referenceId)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, ReferenceId: {@referenceId}"),
            organizationId, personId, referenceId);

        var reference = await References.Where(x =>
                x.OrganizationId == organizationId && x.PersonId == personId && x.Id == referenceId)
            .FirstOrDefaultAsync();

        if (reference == null)
            return Result.Failed(referenceErrors.ReferenceNotFound(referenceId));

        var references = await References.Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .OrderBy(x => x.Order).ToListAsync();

        references.Remove(reference);

        reference.ObjectState = ObjectState.Deleted;
        Repository.InsertOrUpdateGraph(reference);

        for (var i = 0; i < references.Count; i++)
        {
            references[i].Order = i + 1;
            references[i].ObjectState = ObjectState.Modified;

            Repository.InsertOrUpdateGraph(references[i]);
        }

        var results = Repository.Commit();
        if (results > 0)
            return Result.Success();

        return Result.Failed();
    }

    private async Task<int> GetNextReferenceId(int organizationId, int personId)
    {
        var id = await References.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}