#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

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
using System.Security.Claims;

namespace ResumePro.Testing.Bases;

// Simple configuration classes to replace Duende dependencies
public class LocalClientConfiguration
{
    public LocalClientConfiguration(string id, string secret)
    {
        Id = id;
        Secret = secret;
    }

    public string Id { get; }
    public string Secret { get; }
}

public class LocalUserLoginConfiguration
{
    public LocalUserLoginConfiguration(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    public string Password { get; }
}

public class LocalTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; } = 3600;
    public string TokenType { get; set; } = "Bearer";
}

public abstract class IntegrationTest<TFixture, TStartup> where TStartup : class
{
    private LocalClientConfiguration _clientConfiguration;
    private DbContextOptions<ApplicationContext> _context;
    private TestServer _identityServer;

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
        _clientConfiguration = new LocalClientConfiguration("postman", "secret");

        // Create a simplified test server for authentication
        var webHostBuilder = WebHost.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<SimpleProfileService>();
                services.AddSingleton<SimpleResourceOwnerPasswordValidator>();
            })
            .Configure(app => { });

        _identityServer = new TestServer(webHostBuilder);
    }

    private void InitializeApi()
    {
        var apiWebHostBuilder = WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration(CustomWebHostBuilderExtensions.Configure<TFixture>)
            .ConfigureServices(services =>
                services.AddSingleton(_identityServer.CreateHandler()))
            .UseStartup<TStartup>();

        var apiServer = new TestServer(apiWebHostBuilder);

        ServiceProvider = apiServer.Services;

        _context = ServiceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>();

        ApiClient = apiServer.CreateClient();
    }

    protected async Task<string> GetAccessToken(string username, string password)
    {
        // Simplified token generation for testing
        var userLogin = new LocalUserLoginConfiguration(username, password);
        
        // Create a mock token response
        var tokenResponse = new LocalTokenResponse
        {
            AccessToken = GenerateTestToken(username),
            ExpiresIn = 7200,
            TokenType = "Bearer"
        };

        return tokenResponse.AccessToken;
    }
    
    private string GenerateTestToken(string username)
    {
        // This is a simplified mock token for testing purposes
        // In a real implementation, you would use a proper JWT library
        return $"test_token_{username}_{DateTime.UtcNow.Ticks}";
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
