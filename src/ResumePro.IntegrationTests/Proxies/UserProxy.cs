using Bespoke.IntegrationTesting.Bases;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class UserProxy : BaseProxy, IUserController
{
    private const string RootUrl = "v1.0/user";

    public UserProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<UserOutput> GetUser()
    {
        return await DoGetAsync<UserOutput>(RootUrl);
    }
}
