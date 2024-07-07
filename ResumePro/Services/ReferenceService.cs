#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Azure.Amqp.Serialization;
using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Services;


public class ReferenceService : BaseService<Reference>, IReferenceService
{
    public ReferenceService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Reference> References => Repository.Queryable();

    public Task<List<T>> GetReferences<T>(int organizationId, int personId) where T : ReferenceDto
    {
        return References.AsNoTracking()
            .Where(x=>x.OrganizationId == organizationId && x.PersonaId == personId)
            .OrderBy(x=>x.Order)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public Task<OneOf<ReferenceDto, Result>> CreateReference(int organizationId, int personId, ReferenceOptions options)
    {
        throw new NotImplementedException();
    }
}