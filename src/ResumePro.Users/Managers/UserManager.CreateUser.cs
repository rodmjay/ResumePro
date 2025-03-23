#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<IdentityResult> CreateAsync(User user)
    {
        ThrowIfDisposed();
        await UpdateSecurityStampInternal(user);
        var result = await ValidateUser(user);
        if (!result.Succeeded) return result;
        if (Options.Lockout.AllowedForNewUsers && SupportsUserLockout)
            await _userService.SetLockoutEnabledAsync(user, true, CancellationToken);

        // set two-factor enabled here 

        await UpdateNormalizedUserNameAsync(user);
        await UpdateNormalizedEmailAsync(user);

        return await _userService.CreateAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> CreateAsync(User user, string password)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (password == null) throw new ArgumentNullException(nameof(password));
        var result = await UpdatePasswordHash(user, password, true);
        if (!result.Succeeded) return result;
        return await CreateAsync(user);
    }
}