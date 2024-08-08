#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Reflection;
using System.Security.Claims;
using AutoMapper;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using EventAggregator.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ResumeBuilder.App.Extensions;

namespace ResumeBuilder.App;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        Assembly assembly = typeof(Program).Assembly;

        MapperConfiguration config = new MapperConfiguration(x => x.AddMaps(assembly));

        IMapper mapper = config.CreateMapper();

        builder.Services.TryAddSingleton(config);
        builder.Services.TryAddScoped(sp => mapper);

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


        builder.Services.AddScoped<IEventAggregator, EventAggregator.Blazor.EventAggregator>();
        builder.Services.Configure<EventAggregatorOptions>(x => x.AutoRefresh = true);



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