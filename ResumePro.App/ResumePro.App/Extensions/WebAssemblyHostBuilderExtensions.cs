#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ResumePro.App.MessageHandlers;

namespace ResumePro.App.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static WebAssemblyHostBuilder AddProxies(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

        builder.Services
            .AddTransient<ApiAuthorizationMessageHandler>();

        var url = new Uri(builder.Configuration["ApiBase"]);

        //builder.Services.AddHttpClient<IPeopleController, PeopleProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<IApplicationLanguagesController, ApplicationLanguagesProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<IApplicationUsersController, ApplicationUsersProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<ILanguagesController, LanguagesProxy>(
        //    client => client.BaseAddress = url);

        //builder.Services.AddHttpClient<IApplicationPhrasesController, ApplicationPhrasesProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<IApplicationTranslationsController, ApplicationTranslationsProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<IApplicationConsumptionController, ApplicationConsumptionProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<IStripeController, StripeProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<IUserController, UserProxy>(
        //        client => client.BaseAddress = url)
        //    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

        //builder.Services.AddHttpClient<TranslationsProxy>(
        //    client => client.BaseAddress = translationsUrl);

        return builder;
    }
}