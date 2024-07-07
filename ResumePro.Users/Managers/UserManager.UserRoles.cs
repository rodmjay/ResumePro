#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    private IdentityResult UserAlreadyInRoleError(string role)
    {
        _logger.LogWarning(GetLogMessage("User is already in role {role}."), role);
        return IdentityResult.Failed(ErrorDescriber.UserAlreadyInRole(role));
    }

    private IdentityResult UserNotInRoleError(string role)
    {
        _logger.LogWarning(GetLogMessage("User is not in role {role}."), role);
        return IdentityResult.Failed(ErrorDescriber.UserNotInRole(role));
    }

    public override Task<bool> IsInRoleAsync(User user, string role)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return _userService.IsInRoleAsync(user, NormalizeName(role), CancellationToken);
    }

    public override async Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roles)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (roles == null) throw new ArgumentNullException(nameof(roles));

        foreach (var role in roles.Distinct())
        {
            var normalizedRole = NormalizeName(role);
            if (await _userService.IsInRoleAsync(user, normalizedRole, CancellationToken))
                return UserAlreadyInRoleError(role);
            await _userService.AddToRoleAsync(user, normalizedRole, CancellationToken);
        }

        return await UpdateUserAsync(user);
    }

    public override async Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles)
    {
        ThrowIfDisposed();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (roles == null)
            throw new ArgumentNullException(nameof(roles));

        foreach (var role in roles)
        {
            var normalizedRole = NormalizeName(role);
            if (!await _userService.IsInRoleAsync(user, normalizedRole, CancellationToken))
                return UserNotInRoleError(role);

            await _userService.RemoveFromRoleAsync(user, normalizedRole, CancellationToken);
        }

        return await UpdateUserAsync(user);
    }


    public override async Task<IdentityResult> AddToRoleAsync(User user, string role)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var normalizedRole = NormalizeName(role);
        if (await _userService.IsInRoleAsync(user, normalizedRole, CancellationToken))
            return UserAlreadyInRoleError(role);

        await _userService.AddToRoleAsync(user, normalizedRole, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override async Task<IdentityResult> RemoveFromRoleAsync(User user, string role)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        var normalizedRole = NormalizeName(role);
        if (!await _userService.IsInRoleAsync(user, normalizedRole, CancellationToken))
            return UserNotInRoleError(role);
        await _userService.RemoveFromRoleAsync(user, normalizedRole, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override async Task<IList<string>> GetRolesAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userService.GetRolesAsync(user, CancellationToken);
    }

    public override Task<IList<User>> GetUsersInRoleAsync(string roleName)
    {
        ThrowIfDisposed();
        if (roleName == null) throw new ArgumentNullException(nameof(roleName));

        return _userService.GetUsersInRoleAsync(NormalizeName(roleName), CancellationToken);
    }
}