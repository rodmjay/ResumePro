#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using AutoMapper;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumePro.App.Extensions;
using ResumePro.App.Services;
using System.Security.Claims;

namespace ResumePro.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var assembly = typeof(Program).Assembly;

        var config = new MapperConfiguration(x => x.AddMaps(assembly));

        var mapper = config.CreateMapper();

        builder.Services.TryAddSingleton(config);
        builder.Services.TryAddScoped(sp => mapper);

        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

        builder.Services.AddOidcAuthentication(options =>
        {
            builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
            builder.Configuration.Bind("UserOptions", options.UserOptions);
            builder.Configuration.Bind("AuthenticationPaths", options.AuthenticationPaths);

            options.UserOptions.NameClaim = ClaimTypes.NameIdentifier;
            options.UserOptions.RoleClaim = ClaimTypes.Role;
        });
        //builder.Services.AddAuthorizationCore(authorizationOptions =>
        //{
        //    authorizationOptions.AddPolicy(
        //        Policies.CanAccessApis,
        //        Policies.CanAccessApi());
        //});

        //builder.Services.AddScoped<TokenExpirationService>();


        builder.AddProxies();

        AddBlazorise(builder.Services);

        await builder.Build().RunAsync();


        void AddBlazorise(IServiceCollection services)
        {
            services
                .AddBlazorise();
            services
                .AddBootstrap5Providers()
                .AddFontAwesomeIcons();
        }
    }
}