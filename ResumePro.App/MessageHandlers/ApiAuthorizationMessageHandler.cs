#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace ResumePro.App.MessageHandlers;

public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public ApiAuthorizationMessageHandler(
        IAccessTokenProvider provider, NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        ConfigureHandler(
            new[] {config["ApiBase"]});
    }
}


public class AiApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public AiApiAuthorizationMessageHandler(
        IAccessTokenProvider provider, NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        string api = config["AIApiBase"];
        ConfigureHandler(
            new[] { api });
    }
}