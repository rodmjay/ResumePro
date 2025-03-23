#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using ResumePro.Users.Interfaces;
using IdentityModel;

namespace ResumePro.Testing.Services;

public class SimpleProfileService : ILocalProfileService
{
    public Task GetProfileDataAsync(LocalProfileDataRequestContext context)
    {
        var subject = context.Subject.Claims.First(claim => claim.Type == JwtClaimTypes.Subject).Value;

        context.IssuedClaims =
        [
            new(JwtClaimTypes.Subject, subject),
            new(JwtClaimTypes.Scope, "profile"),
            new(JwtClaimTypes.Scope, "openid"),
            new(JwtClaimTypes.Scope, "api1"),
            new("iss", "http://localhost"),
            new Claim("organizationId", "1")
        ];

        return Task.CompletedTask;
    }

    public Task IsActiveAsync(LocalIsActiveContext context)
    {
        context.IsActive = true;
        return Task.CompletedTask;
    }
}
