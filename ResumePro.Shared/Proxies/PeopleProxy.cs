using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Helpers;
using ResumePro.Shared.Options;

namespace ResumePro.Shared.Proxies
{
    public class PeopleProxy : BaseProxy, IPeopleController
    {
        public PeopleProxy(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<PagedList<PersonaDto>> GetPeople(PersonaFilters filters, PagingQuery paging)
        {
            var querystring = UrlHelper.CombineObjectsToUrl(paging, filters);

            return await DoPost<PersonaFilters, PagedList<PersonaDto>>($"v1.0/people/search?{querystring}", filters);
        }

        public async Task<PersonaDetails> GetPerson(int personId)
        {
            return await DoGet<PersonaDetails>($"v1.0/people/{personId}");
        }

        public async Task<ActionResult<PersonaDetails>> CreatePerson(PersonaOptions options)
        {
            return await DoPost<PersonaOptions, PersonaDetails>("v1.0/people", options);
        }

        public async Task<ActionResult<PersonaDetails>> UpdatePerson(int personId, PersonaOptions options)
        {
            return await DoPut<PersonaOptions, PersonaDetails>("v1.0/people", options);
        }

        public async Task<Result> DeletePerson(int personId)
        {
            return await DoDelete<Result>($"v1.0/people/{personId}");
        }
    }
}
