using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class PersonLanguagesProxy : BaseProxy, IPersonLanguagesController
{
    private const string RootUrl = "v1.0/people/{0}/languages";
    private const string ToggleUrl = "v1.0/people/{0}/languages/{1}/{2}";

    public PersonLanguagesProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<List<PersonaLanguageDto>> GetPersonLanguages(int personId)
    {
        return await DoGetAsync<List<PersonaLanguageDto>>(string.Format(RootUrl, personId));
    }
    
    public async Task<Result> ToggleLanguage(int personId, int languageId, string proficiency)
    {
        return await DoPostAsync<Result>(string.Format(ToggleUrl, personId, languageId, proficiency));
    }
}
