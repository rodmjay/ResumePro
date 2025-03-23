using System.Diagnostics.CodeAnalysis;
using Bespoke.Data.Enums;
using Bespoke.Data.Extensions;
using Bespoke.Data.Interfaces;
using Bespoke.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResumePro.Services.ErrorDescribers;
using ResumePro.Services.Interfaces;
using ResumePro.Shared.Enums;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Implementations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public sealed class PersonaLanguageService : BaseService<PersonaLanguage>, IPersonaLanguageService
{
    private readonly LanguageErrorDescriber _languageErrors;
    private readonly IRepositoryAsync<Language> _languageRepo;

    public PersonaLanguageService(IServiceProvider serviceProvider,
        LanguageErrorDescriber languageErrors,
        IRepositoryAsync<Language> languageRepo) : base(serviceProvider)
    {
        _languageErrors = languageErrors;
        _languageRepo = languageRepo;
    }

    private IQueryable<PersonaLanguage> PersonaLanguages => Repository.Queryable();
    private IQueryable<Language> Languages => _languageRepo.Queryable();

    public Task<T> GetPersonaLanguage<T>(int organizationId, int personId, string language) where T : PersonaLanguageDto
    {
        return PersonaLanguages.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ProjectTo<T>(Mapper)
            .FirstAsync();
    }

    public Task<List<T>> GetPersonaLanguages<T>(int organizationId, int personId) where T : PersonaLanguageDto
    {
        return PersonaLanguages.AsNoTracking()
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId)
            .ProjectTo<T>(Mapper)
            .ToListAsync();
    }

    public async Task<OneOf<PersonaLanguageDto, Result>> CreateOrUpdatePersonaLanguage(int organizationId, int personId,
        PersonLanguageOptions options)
    {
        Logger.LogInformation(
            GetLogMessage("OrganizationId: {@organizationId}, PersonId: {@personId}, Options: {@options}"));

        var errors = new List<Error>();

        await foreach (var error in GetErrors(options.LanguageId)) errors.Add(error);

        if (errors.Any())
            return Result.Failed(errors.ToArray());

        var pl = await PersonaLanguages.Where(x =>
                x.OrganizationId == organizationId && x.PersonId == personId && x.Code3 == options.LanguageId)
            .FirstOrDefaultAsync();

        if (pl == null)
            pl = new PersonaLanguage
            {
                ObjectState = ObjectState.Added,
                PersonId = personId,
                OrganizationId = organizationId
            };
        else
            pl.ObjectState = ObjectState.Modified;

        pl.Code3 = options.LanguageId;
        pl.Proficiency = options.Proficiency;

        var records = Repository.InsertOrUpdateGraph(pl, true);
        if (records > 0)
            return await GetPersonaLanguage<PersonaLanguageDto>(organizationId, personId, options.LanguageId);

        return Result.Failed();
    }
    
    public async Task<Result> ToggleLanguage(int organizationId, int personId, int languageId, string proficiency)
    {
        Logger.LogInformation(
            GetLogMessage($"Toggling language - OrganizationId: {organizationId}, PersonId: {personId}, LanguageId: {languageId}, Proficiency: {proficiency}"));
            
        // Get the language by ID
        var language = await Languages.AsNoTracking()
            .Where(x => x.Code3 == languageId.ToString())
            .FirstOrDefaultAsync();
            
        if (language == null)
            return Result.Failed(_languageErrors.LanguageNotFound(languageId.ToString()));
            
        // Check if the person already has this language
        var personLanguage = await PersonaLanguages
            .Where(x => x.OrganizationId == organizationId && x.PersonId == personId && x.Code3 == language.Code3)
            .FirstOrDefaultAsync();
            
        // If the language exists for this person, remove it (toggle off)
        if (personLanguage != null)
        {
            Repository.Delete(personLanguage);
            var deleteResult = Repository.InsertOrUpdateGraph(personLanguage, true);
            return deleteResult > 0 ? Result.Success() : Result.Failed();
        }
        
        // Otherwise, add the language (toggle on)
        var newPersonLanguage = new PersonaLanguage
        {
            ObjectState = ObjectState.Added,
            PersonId = personId,
            OrganizationId = organizationId,
            Code3 = language.Code3,
            Proficiency = Enum.Parse<LanguageLevel>(proficiency)
        };
        
        var records = Repository.InsertOrUpdateGraph(newPersonLanguage, true);
        return records > 0 ? Result.Success() : Result.Failed();
    }

    private async IAsyncEnumerable<Error> GetErrors(string language)
    {
        var languageExists = await Languages
            .AsNoTracking()
            .Where(x => x.Code3 == language).AnyAsync();

        if (!languageExists)
            yield return _languageErrors.LanguageNotFound(language);
    }
}
