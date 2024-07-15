#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Queries;
using ResumePro.Extensions;
using ResumePro.Shared.Filters;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PeopleService : BaseService<Persona>, IPeopleService
{
    private readonly PersonErrorDescriber _personErrors;

    public PeopleService(IServiceProvider serviceProvider, PersonErrorDescriber personErrors) : base(serviceProvider)
    {
        _personErrors = personErrors;
    }

    private IQueryable<Persona> People => Repository.Queryable();

    public async Task<PagedList<T>> GetPeople<T>(int organizationId, PersonaFilters filters, PagingQuery paging)
        where T : PersonaDto
    {
        filters ??= new PersonaFilters();

        var expr = filters.GetExpression()
            .And(x => x.OrganizationId == organizationId);

        return await this.PaginateAsync<Persona, T>(expr, paging);
    }

    public Task<T> GetPerson<T>(int organizationId, int personId) where T : PersonaDto
    {
        return People.AsNoTracking().Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .AsSplitQuery()
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public async Task<Result> DeletePerson(int organizationId, int personId)
    {
        var person = await People.Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .FirstOrDefaultAsync();

        if (person == null)
            return Result.Failed(_personErrors.PersonNotFound(personId));

        person.IsDeleted = true;
        person.ObjectState = ObjectState.Modified;

        var results = Repository.InsertOrUpdateGraph(person, true);
        if (results > 0)
            return Result.Success();

        return Result.Failed(_personErrors.UnableToSavePerson());
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

        return Result.Failed(_personErrors.UnableToSavePerson());
    }

    public async Task<OneOf<PersonaDetails, Result>> UpdatePerson(int organizationId, int personId,
        PersonaOptions options)
    {
        var person = await People.Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .FirstOrDefaultAsync();

        if (person == null)
            return Result.Failed(_personErrors.PersonNotFound(personId));

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

        return Result.Failed(_personErrors.UnableToSavePerson());
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