using ResumePro.Languages.Entities;
using ResumePro.Shared.Models;

namespace ResumePro.Services;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PersonaLanguageService(IServiceProvider serviceProvider, LanguageErrorDescriber languageErrors, IRepositoryAsync<Language> languageRepo)
    : BaseService<PersonaLanguage>(serviceProvider), IPersonaLanguageService
{
    private IQueryable<PersonaLanguage> PersonaLanguages => Repository.Queryable();
    private IQueryable<Language> Languages => languageRepo.Queryable();
    public Task<T> GetPersonaLanguage<T>(int organizationId, int personId, string language) where T : PersonaLanguageDto
    {
        return PersonaLanguages.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .ProjectTo<T>(Mapper)
            .FirstOrDefaultAsync();
    }

    public Task<List<T>> GetPersonaLanguages<T>(int organizationId, int personId) where T : PersonaLanguageDto
    {
        return PersonaLanguages.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonaId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    private async IAsyncEnumerable<Error> GetErrors(string language)
    {
        var languageExists = await Languages
            .AsNoTracking()
            .Where(x => x.Code3 == language).AnyAsync();

        if (!languageExists)
            yield return languageErrors.LanguageNotFound(language);
    }

    public async Task<OneOf<PersonaLanguageDto, Result>> CreateOrUpdatePersonaLanguage(int organizationId, int personId, PersonLanguageOptions options)
    {
        var errors = new List<Error>();

        await foreach (Error error in GetErrors(options.LanguageId))
        {
            errors.Add(error);
        }

        if (errors.Any())
            return Result.Failed(errors.ToArray());

        var pl = await PersonaLanguages.Where(x => x.OrganizationId == organizationId && x.PersonaId == personId && x.Code3 == options.LanguageId)
            .FirstOrDefaultAsync();

        if (pl == null)
        {
            pl = new PersonaLanguage()
            {
                ObjectState = ObjectState.Added,
                PersonaId = personId,
                OrganizationId = organizationId
            };
        }
        else
        {
            pl.ObjectState = ObjectState.Modified;
        }

        pl.Code3 = options.LanguageId;
        pl.Proficiency = options.Proficiency;

        var records = Repository.InsertOrUpdateGraph(pl, true);
        if (records > 0)
        {
            return await GetPersonaLanguage<PersonaLanguageDto>(organizationId, personId, options.LanguageId);
        }

        return Result.Failed();
    }

}