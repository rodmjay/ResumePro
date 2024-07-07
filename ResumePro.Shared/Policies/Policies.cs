#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Authorization;

namespace ResumePro.Shared.Policies;

public static class Policies
{
    public const string CanAccessApis = "CanAccessApi";

    public static AuthorizationPolicy CanAccessApi()
    {
        return new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    }
}