#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<IList<Claim>> GetClaimsAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await _userService.GetClaimsAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> ReplaceClaimAsync(User user, Claim claim, Claim newClaim)
    {
        ThrowIfDisposed();
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        if (newClaim == null) throw new ArgumentNullException(nameof(newClaim));
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _userService.ReplaceClaimAsync(user, claim, newClaim, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override Task<IdentityResult> RemoveClaimAsync(User user, Claim claim)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (claim == null) throw new ArgumentNullException(nameof(claim));

        return RemoveClaimsAsync(user, new[] {claim});
    }

    public override async Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
    {
        ThrowIfDisposed();
        if (claims == null) throw new ArgumentNullException(nameof(claims));
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _userService.AddClaimsAsync(user, claims, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override Task<IdentityResult> AddClaimAsync(User user, Claim claim)
    {
        ThrowIfDisposed();
        if (claim == null) throw new ArgumentNullException(nameof(claim));
        if (user == null) throw new ArgumentNullException(nameof(user));

        return AddClaimsAsync(user, new[] {claim});
    }

    public override async Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (claims == null) throw new ArgumentNullException(nameof(claims));

        await _userService.RemoveClaimsAsync(user, claims, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override Task<IList<User>> GetUsersForClaimAsync(Claim claim)
    {
        ThrowIfDisposed();
        if (claim == null) throw new ArgumentNullException(nameof(claim));

        return _userService.GetUsersForClaimAsync(claim, CancellationToken);
    }
}