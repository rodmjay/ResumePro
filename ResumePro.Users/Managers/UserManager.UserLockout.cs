#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<bool> IsLockedOutAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (!await _userService.GetLockoutEnabledAsync(user, CancellationToken)) return false;

        var lockoutTime = await _userService.GetLockoutEndDateAsync(user, CancellationToken);
        return lockoutTime >= DateTimeOffset.UtcNow;
    }

    public override async Task<IdentityResult> SetLockoutEnabledAsync(User user, bool enabled)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _userService.SetLockoutEnabledAsync(user, enabled, CancellationToken);

        return await UpdateUserAsync(user);
    }

    public override async Task<bool> GetLockoutEnabledAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userService.GetLockoutEnabledAsync(user, CancellationToken);
    }

    public override async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userService.GetLockoutEndDateAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (!await _userService.GetLockoutEnabledAsync(user, CancellationToken))
        {
            Logger.LogWarning(
                GetLogMessage("Lockout for user failed because lockout is not enabled for this user."));
            return IdentityResult.Failed(ErrorDescriber.UserLockoutNotEnabled());
        }

        await _userService.SetLockoutEndDateAsync(user, lockoutEnd, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override async Task<IdentityResult> AccessFailedAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        // If this puts the user over the threshold for lockout, lock them out and reset the access failed count
        var count = await _userService.IncrementAccessFailedCountAsync(user, CancellationToken);
        if (count < Options.Lockout.MaxFailedAccessAttempts) return await UpdateUserAsync(user);
        Logger.LogWarning(GetLogMessage("User is locked out."));
        await _userService.SetLockoutEndDateAsync(user,
            DateTimeOffset.UtcNow.Add(Options.Lockout.DefaultLockoutTimeSpan),
            CancellationToken);
        await _userService.ResetAccessFailedCountAsync(user, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override async Task<IdentityResult> ResetAccessFailedCountAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (await GetAccessFailedCountAsync(user) == 0) return IdentityResult.Success;
        await _userService.ResetAccessFailedCountAsync(user, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override async Task<int> GetAccessFailedCountAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userService.GetAccessFailedCountAsync(user, CancellationToken);
    }
}