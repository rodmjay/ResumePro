#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.Extensions.Logging;
using ResumePro.Core.Queries;
using ResumePro.Extensions;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PeopleService(IServiceProvider serviceProvider, PersonErrorDescriber personErrors,
    IRepositoryAsync<StateProvince> stateRepo, GeographyErrorDescriber geoErrors)
    : BaseService<Persona>(serviceProvider), IPeopleService
{
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
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}"), organizationId,
            personId);

        var person = await People.Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .FirstOrDefaultAsync();

        if (person == null)
            return Result.Failed(personErrors.PersonNotFound(personId));

        person.IsDeleted = true;
        person.ObjectState = ObjectState.Modified;

        var results = Repository.InsertOrUpdateGraph(person, true);
        if (results > 0)
            return Result.Success();

        return Result.Failed(personErrors.UnableToSavePerson());
    }


    private async IAsyncEnumerable<Error> GetErrors(PersonaOptions options)
    {
        var stateExists = await stateRepo.Queryable()
            .AsNoTracking()
            .Where(x => x.Id == options.StateId).AnyAsync();

        if (!stateExists)
            yield return geoErrors.StateNotFound();
    }

    public async Task<OneOf<PersonaDetails, Result>> CreatePerson(int organizationId, PersonaOptions options)
    {
        Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, Options: {@options}"), organizationId,
            options);


        var errors = new List<Error>();

        await foreach (var error in GetErrors(options)) errors.Add(error);

        if (errors.Any())
            return Result.Failed(errors.ToArray());

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

        return Result.Failed(personErrors.UnableToSavePerson());
    }

    public async Task<OneOf<PersonaDetails, Result>> UpdatePerson(int organizationId, int personId,
        PersonaOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
            organizationId, personId, options);

        var person = await People.Where(x => x.OrganizationId == organizationId && x.Id == personId)
            .FirstOrDefaultAsync();

        if (person == null)
            return Result.Failed(personErrors.PersonNotFound(personId));

        var errors = new List<Error>();

        await foreach (var error in GetErrors(options)) errors.Add(error);

        if (errors.Any())
            return Result.Failed(errors.ToArray());

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

        return Result.Failed(personErrors.UnableToSavePerson());
    }

    private async Task<int> GetNextPersonId(int organizationId)
    {
        var id = await People.AsNoTracking()
            .IgnoreQueryFilters()
            .Where(x => x.OrganizationId == organizationId)
            .OrderByDescending(x => x.Id)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
        return id + 1;
    }
}