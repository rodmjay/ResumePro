﻿#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json.Linq;

namespace ResumePro.App.Services;

public class TokenExpirationService
{
    private readonly NavigationManager _navigationManager;
    private readonly SessionStorageInterop _sessionStorage;

    private readonly string AuthSessionKey = "";
    private readonly SignOutSessionStateManager SignOutManager;
    private Timer _timer;

    public TokenExpirationService(
        SignOutSessionStateManager signOutSessionStateManager,
        SessionStorageInterop sessionStorage,
        IConfiguration configuration,
        NavigationManager navigationManager)
    {
        SignOutManager = signOutSessionStateManager;
        AuthSessionKey = configuration["AuthSessionKey"];
        _sessionStorage = sessionStorage;
        _navigationManager = navigationManager;
    }

    public void StartTokenExpirationTimer()
    {
        // Set the timer interval to check for token expiration
        _timer = new Timer(CheckExpiration, null, 0, 10000);
    }

    private async void CheckExpiration(object state)
    {
        string authValue = await _sessionStorage.LoadFromSessionStorage<string>(AuthSessionKey);
        if (authValue != null)
        {
            JObject obj = JObject.Parse(authValue);

            string expiration = obj["expires_at"].Value<string>();

            if (expiration != null && long.TryParse(expiration, out long expirationTime))
            {
                long currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                long timeUntilExpiration = expirationTime - currentTime;

                if (timeUntilExpiration <= 300)
                {
                    await SignOutManager.SetSignOutState();
                    _navigationManager.NavigateTo("authentication/logout");
                }
            }
        }
    }
}