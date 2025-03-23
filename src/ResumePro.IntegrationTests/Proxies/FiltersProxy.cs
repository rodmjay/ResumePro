using Bespoke.IntegrationTesting.Bases;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class FiltersProxy : BaseProxy, IFiltersController
{
    private const string RootUrl = "v1.0/filters";

    public FiltersProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<FilterContainer> GetFilters()
    {
        return await DoGetAsync<FilterContainer>(RootUrl);
    }
}
