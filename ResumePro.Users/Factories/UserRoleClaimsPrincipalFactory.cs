#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ResumePro.Users.Entities;
using ResumePro.Users.Managers;

namespace ResumePro.Users.Factories;

public class UserRoleClaimsPrincipalFactory : UserClaimsPrincipalFactory
{
    private readonly RoleManager _roleManager;
    private readonly UserManager _userManager;

    public UserRoleClaimsPrincipalFactory(UserManager userManager, RoleManager roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
    {
        var id = await base.GenerateClaimsAsync(user);

        if (_userManager.SupportsUserRole)
        {
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
                if (_roleManager.SupportsRoleClaims)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null) id.AddClaims(await _roleManager.GetClaimsAsync(role));
                }
            }
        }

        return id;
    }
}