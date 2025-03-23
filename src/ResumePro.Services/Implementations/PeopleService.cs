using System.Diagnostics.CodeAnalysis;
using Bespoke.Core.Builders;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.ErrorDescribers;
using ResumePro.Services.Helpers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations
{
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public sealed class PeopleService : BaseService<Persona>, IPeopleService
    {
        private readonly GeographyErrorDescriber _geoErrors;
        private readonly PersonErrorDescriber _personErrors;
        private readonly IRepositoryAsync<StateProvince> _stateRepo;

        public PeopleService(IServiceProvider serviceProvider,
            PersonErrorDescriber personErrors,
            IRepositoryAsync<StateProvince> stateRepo,
            GeographyErrorDescriber geoErrors) : base(serviceProvider)
        {
            _personErrors = personErrors;
            _stateRepo = stateRepo;
            _geoErrors = geoErrors;
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
            return People.AsNoTracking()
                .Where(x => x.OrganizationId == organizationId && x.Id == personId)
                .AsSplitQuery()
                .ProjectTo<T>(Mapper)
                .FirstAsync();
        }

        public async Task<Result> DeletePerson(int organizationId, int personId)
        {
            Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}"), organizationId, personId);

            var person = await People
                .Where(x => x.OrganizationId == organizationId && x.Id == personId)
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

        public async Task<OneOf<PersonaDetails, Result>> CreatePerson(int organizationId, PersonOptions options)
        {
            Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, Options: {@options}"), organizationId, options);

            var errors = new List<Error>();

            await foreach (var error in GetErrors(options))
                errors.Add(error);

            if (errors.Any())
                return Result.Failed(errors.ToArray());

            var nextId = await GetNextPersonId(organizationId);
            var person = new Persona
            {
                Id = nextId,
                ObjectState = ObjectState.Added,
                PhoneNumber = options.PhoneNumber,
                OrganizationId = organizationId,
                City = options.City,
                StateId = options.StateId,
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                GitHub = options.GitHub,
                LinkedIn = options.LinkedIn
            };

            // Handle language options for create
            HandleLanguageOptions(person, organizationId, options.LanguageOptions, isUpdate: false);

            var result = Repository.InsertOrUpdateGraph(person, true);
            if (result > 0)
                return await GetPerson<PersonaDetails>(organizationId, person.Id);

            return Result.Failed(_personErrors.UnableToSavePerson());
        }

        public async Task<OneOf<PersonaDetails, Result>> UpdatePerson(int organizationId, int personId, PersonOptions options)
        {
            Logger.LogInformation(GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"),
                organizationId, personId, options);

            var person = await People
                .Include(x => x.Languages)
                .Where(x => x.OrganizationId == organizationId && x.Id == personId)
                .FirstOrDefaultAsync();

            if (person == null)
                return Result.Failed(_personErrors.PersonNotFound(personId));

            var errors = new List<Error>();

            await foreach (var error in GetErrors(options))
                errors.Add(error);

            if (errors.Any())
                return Result.Failed(errors.ToArray());

            person.ObjectState = ObjectState.Modified;
            person.Email = options.Email;
            person.FirstName = options.FirstName;
            person.LastName = options.LastName;
            person.GitHub = options.GitHub;
            person.PhoneNumber = options.PhoneNumber;
            person.StateId = options.StateId;
            person.City = options.City;

            // Handle language options for update
            HandleLanguageOptions(person, organizationId, options.LanguageOptions, isUpdate: true);

            var results = Repository.InsertOrUpdateGraph(person, true);
            if (results > 0)
                return await GetPerson<PersonaDetails>(organizationId, personId);

            return Result.Failed(_personErrors.UnableToSavePerson());
        }

        /// <summary>
        /// Handles the language options for a person.
        /// For updates, existing language entries are marked as deleted before processing.
        /// For each language option, if a matching language is found, its state is updated;
        /// otherwise, a new language entry is added.
        /// </summary>
        /// <param name="person">The person entity to update.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="languageOptions">The language options provided in the request.</param>
        /// <param name="isUpdate">If true, marks all existing language records as deleted prior to processing.</param>
        private void HandleLanguageOptions(Persona person, int organizationId, IEnumerable<PersonLanguageOptions> languageOptions, bool isUpdate)
        {
            if (isUpdate)
            {
                foreach (var lang in person.Languages)
                    lang.ObjectState = ObjectState.Deleted;
            }

            foreach (var langOption in languageOptions)
            {
                var language = person.Languages.FirstOrDefault(x => x.Code3 == langOption.LanguageId);
                if (language == null)
                {
                    language = new PersonaLanguage
                    {
                        ObjectState = ObjectState.Added,
                        OrganizationId = organizationId,
                        PersonId = person.Id,
                        Code3 = langOption.LanguageId
                    };
                    person.Languages.Add(language);
                }
                else
                {
                    language.ObjectState = ObjectState.Modified;
                }
                language.Proficiency = langOption.Proficiency;
            }
        }

        private async IAsyncEnumerable<Error> GetErrors(PersonOptions options)
        {
            // Always validate the state ID
            var stateExists = await _stateRepo.Queryable()
                .AsNoTracking()
                .Where(x => x.Id == options.StateId)
                .AnyAsync();

            if (!stateExists)
                yield return _geoErrors.StateNotFound();
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
}
