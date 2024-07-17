#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using IdentityModel.Client;
using NUnit.Framework;
using ResumePro.Interfaces;
using ResumePro.Shared.Proxies;
using ResumePro.Testing.Bases;

namespace ResumePro.Api.Testing;

public abstract class BaseApiTest : IntegrationTest<BaseApiTest, Startup>
{

    [OneTimeSetUp]
    public virtual async Task SetupFixture()
    {
        await ResetDatabase();
        var accessToken = await GetAccessToken("admin@admin.com", "ASDFasdf!");
        ApiClient.SetBearerToken(accessToken);
    }
    
    [OneTimeTearDown]
    public virtual async Task TeardownFixture()
    {
        await DeleteDatabase();
    }

    protected IPeopleController PeopleProxy => new PeopleProxy(ApiClient);
    


}