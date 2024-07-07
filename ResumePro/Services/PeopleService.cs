#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using OneOf;
using ResumePro.Core.Data.Enums;
using ResumePro.Core.Queries;
using ResumePro.Core.Services.Bases;
using ResumePro.Entities;
using ResumePro.Extensions;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Options;

namespace ResumePro.Services;

public class PeopleService : BaseService<Persona>, IPeopleService
{
    public PeopleService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    private IQueryable<Persona> People => Repository.Queryable();

    public async Task<PagedList<T>> GetPeople<T>(int organizationId, PersonaFilters filters, PagingQuery paging)
        where T : PersonaDto
    {
        if (filters == null) filters = new PersonaFilters();

        var expr = filters.GetExpression()
            .And(x => x.OrganizationId == organizationId);

        return await this.PaginateAsync<Persona, T>(expr, paging);
    }

    public Task<T> GetPerson<T>(int organizationId, int personId) where T : PersonaDto
    {
        return People.AsNoTracking().Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<Result> DeletePerson(int organizationId, int personId)
    {
        var person = await People.Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .FirstOrDefaultAsync();

        if (person == null)
            return Result.Failed();

        person.IsDeleted = true;
        person.ObjectState = ObjectState.Modified;

        var results = Repository.InsertOrUpdateGraph(person, true);
        if (results > 0)
            return Result.Success();

        return Result.Failed();
    }

    public async Task<OneOf<PersonaDetails, Result>> CreatePerson(int organizationId, PersonaOptions options)
    {
        var nextId = await GetNextPersonId(organizationId);
        var person = new Persona
        {
            Id = nextId,
            ObjectState = ObjectState.Added,
            OrganizationId = organizationId,
            City = options.City,
            StateId = options.StateId,
            FirstName = options.FirstName,
            LastName = options.LastName,
            Email = options.Email,
            GitHub = options.GitHub,
            LinkedIn = options.LinkedIn
        };

        var result = Repository.InsertOrUpdateGraph(person, true);
        if (result > 0) return await GetPerson<PersonaDetails>(organizationId, person.Id);

        return Result.Failed();
    }

    public async Task<OneOf<PersonaDetails, Result>> UpdatePerson(int organizationId, int personId,
        PersonaOptions options)
    {
        var person = await People.Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .FirstOrDefaultAsync();

        if (person == null)
            return Result.Failed();

        person.ObjectState = ObjectState.Modified;
        person.Email = options.Email;
        person.FirstName = options.FirstName;
        person.LastName = options.LastName;
        person.GitHub = options.GitHub;
        person.StateId = options.StateId;
        person.City = options.City;

        var results = Repository.InsertOrUpdateGraph(person, true);
        if (results > 0)
            return await GetPerson<PersonaDetails>(organizationId, personId);

        return Result.Failed();
    }

    private async Task<int> GetNextPersonId(int organizationId)
    {
        var result = await People.AsNoTracking()
            .IgnoreQueryFilters()
            .OrderByDescending(x => x.Id)
            .Where(x => x.OrganizationId == organizationId)
            .FirstOrDefaultAsync();

        if (result == null)
            return 1;

        return result.Id + 1;
    }
}