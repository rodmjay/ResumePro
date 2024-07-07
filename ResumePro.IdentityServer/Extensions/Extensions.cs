#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System;
using Duende.IdentityServer.Models;

namespace ResumePro.IdentityServer.Extensions;

public static class Extensions
{
    /// <summary>
    ///     Checks if the redirect URI is for a native client.
    /// </summary>
    /// <returns></returns>
    public static bool IsNativeClient(this AuthorizationRequest context)
    {
        return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
               && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
    }
}