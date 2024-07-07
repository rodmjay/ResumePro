#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class SignInManager
{
    public override async Task RefreshSignInAsync(User user)
    {
        var auth = await Context.AuthenticateAsync(IdentityConstants.ApplicationScheme);
        IList<Claim> claims = Array.Empty<Claim>();

        var authenticationMethod = auth?.Principal?.FindFirst(ClaimTypes.AuthenticationMethod);
        var amr = auth?.Principal?.FindFirst("amr");

        if (authenticationMethod != null || amr != null)
        {
            claims = new List<Claim>();
            if (authenticationMethod != null) claims.Add(authenticationMethod);
            if (amr != null) claims.Add(amr);
        }

        await SignInWithClaimsAsync(user, auth?.Properties, claims);
    }

    public override Task SignInWithClaimsAsync(User user, bool isPersistent, IEnumerable<Claim> additionalClaims)
    {
        return SignInWithClaimsAsync(user, new AuthenticationProperties {IsPersistent = isPersistent},
            additionalClaims);
    }

    public override async Task SignInWithClaimsAsync(User user, AuthenticationProperties authenticationProperties,
        IEnumerable<Claim> additionalClaims)
    {
        var userPrincipal = await CreateUserPrincipalAsync(user);
        foreach (var claim in additionalClaims) userPrincipal.Identities.First().AddClaim(claim);
        await Context.SignInAsync(Constants.LocalIdentity.DefaultApplicationScheme,
            userPrincipal,
            authenticationProperties ?? new AuthenticationProperties());
    }
}