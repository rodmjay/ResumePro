#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class RoleManager
{
    public override async Task<IdentityResult> AddClaimAsync(Role role, Claim claim)
    {
        ThrowIfDisposed();
        if (claim == null)
            throw new ArgumentNullException(nameof(claim));
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        await _roleService.AddClaimAsync(role, claim, CancellationToken);
        return await UpdateRoleAsync(role);
    }

    public override Task<IList<Claim>> GetClaimsAsync(Role role)
    {
        ThrowIfDisposed();
        if (role == null)
            throw new ArgumentNullException(nameof(role));
        return _roleService.GetClaimsAsync(role, CancellationToken);
    }

    public override async Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim)
    {
        ThrowIfDisposed();
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        await _roleService.RemoveClaimAsync(role, claim, CancellationToken);
        return await UpdateRoleAsync(role);
    }
}