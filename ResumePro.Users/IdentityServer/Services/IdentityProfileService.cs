#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using ResumePro.Users.Factories;
using ResumePro.Users.Managers;

namespace ResumePro.Users.IdentityServer.Services;

public class IdentityProfileService : IProfileService
{
    private readonly UserRoleClaimsPrincipalFactory _userClaimsFactory;
    private readonly UserManager _userManager;

    public IdentityProfileService(
        UserManager userManager,
        UserRoleClaimsPrincipalFactory userClaimsFactory)
    {
        _userManager = userManager;
        _userClaimsFactory = userClaimsFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        var principal = await _userClaimsFactory.CreateAsync(user);

        var claims = principal.Claims.ToList();

        claims = claims
            .Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

        claims.Add(user.TwoFactorEnabled ? new Claim("amr", "mfa") : new Claim("amr", "pwd"));

        claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, user.Email));
        claims.Add(new Claim("organizationId", user.OrganizationId.ToString()));

        context.IssuedClaims = claims;
    }


    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;
    }
}