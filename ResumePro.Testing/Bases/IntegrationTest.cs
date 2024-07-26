#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using alsami.Duende.IdentityServer.AspNetCore.Testing.Builder;
using alsami.Duende.IdentityServer.AspNetCore.Testing.Configuration;
using alsami.Duende.IdentityServer.AspNetCore.Testing.Services;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework.Legacy;
using ResumePro.Context;
using ResumePro.Shared.Extensions;
using ResumePro.Testing.Extensions;
using ResumePro.Testing.Services;

namespace ResumePro.Testing.Bases;

public abstract class IntegrationTest<TFixture, TStartup> where TStartup : class
{
    private ClientConfiguration _clientConfiguration;
    private DbContextOptions<ApplicationContext> _context;
    private IdentityServerWebHostProxy _identityServerWebHostProxy;

    protected IntegrationTest()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        InitializeIdentityServerProxy();
        InitializeApi();
    }

    protected IServiceProvider ServiceProvider { get; private set; }
    protected HttpClient ApiClient { get; private set; }


    private void InitializeIdentityServerProxy()
    {
        _clientConfiguration = new ClientConfiguration("postman", "secret");

        var client = new Client
        {
            ClientId = _clientConfiguration.Id,
            ClientSecrets = new List<Secret>
            {
                new(_clientConfiguration.Secret.Sha256())
            },
            AllowedScopes = new[] { "api1", "profile", "openid" },
            AllowedGrantTypes = new[] { GrantType.ClientCredentials, GrantType.ResourceOwnerPassword },
            AccessTokenType = AccessTokenType.Jwt,
            AccessTokenLifetime = 7200,
            AllowOfflineAccess = true
        };

        var webHostBuilder = new IdentityServerTestWebHostBuilder()
            .AddClients(client)
            .AddApiScopes(new ApiScope("api1"))
            .AddIdentityResources(new IdentityResources.OpenId(), new IdentityResources.Profile())
            .UseResourceOwnerPasswordValidator(typeof(SimpleResourceOwnerPasswordValidator))
            .UseProfileService(typeof(SimpleProfileService))
            .CreateWebHostBuilder();

        _identityServerWebHostProxy = new IdentityServerWebHostProxy(webHostBuilder);
    }

    private void InitializeApi()
    {
        var apiWebHostBuilder = WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(CustomWebHostBuilderExtensions.Configure<TFixture>)
            .ConfigureServices(services =>
                services.AddSingleton(_identityServerWebHostProxy.IdentityServer.CreateHandler()))
            .UseStartup<TStartup>();

        var apiServer = new TestServer(apiWebHostBuilder);

        ServiceProvider = apiServer.Services;

        _context = ServiceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();

        ApiClient = apiServer.CreateClient();
    }

    protected async Task<string> GetAccessToken(string username, string password)
    {
        var userLogin = new UserLoginConfiguration(username, password);

        var tokenResponse = await _identityServerWebHostProxy
            .GetResourceOwnerPasswordAccessTokenAsync(_clientConfiguration, userLogin, "api1", "profile",
                "openid");

        return tokenResponse.AccessToken;
    }

    protected async Task<TOutput> DoPost<TInput, TOutput>(string url, TInput input)
    {
        var content = input.SerializeToUTF8Json();
        var response = await ApiClient.PostAsync(url, content);
        ClassicAssert.True(response.IsSuccessStatusCode);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoGet<TOutput>(string url)
    {
        var response = await ApiClient.GetAsync(url);
        ClassicAssert.True(response.IsSuccessStatusCode);

        var result = response.Content.DeserializeObject<TOutput>();

        return result;
    }

    protected async Task<TOutput> DoPut<TInput, TOutput>(string url, TInput input)
    {
        var content = input.SerializeToUTF8Json();
        var response = await ApiClient.PutAsync(url, content);
        ClassicAssert.True(response.IsSuccessStatusCode);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task<TOutput> DoDelete<TOutput>(string url)
    {
        var response = await ApiClient.DeleteAsync(url);
        ClassicAssert.True(response.IsSuccessStatusCode);

        var result = response.Content.DeserializeObject<TOutput>();
        return result;
    }

    protected async Task ResetDatabase()
    {
        await using var context = new ApplicationContext(_context);
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();
    }

    protected async Task DeleteDatabase()
    {
        await using var context = new ApplicationContext(_context);
        await context.Database.EnsureDeletedAsync();
    }
}