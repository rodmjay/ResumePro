#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userService.GetLoginsAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login)
    {
        ThrowIfDisposed();
        if (login == null) throw new ArgumentNullException(nameof(login));
        if (user == null) throw new ArgumentNullException(nameof(user));

        var existingUser = await FindByLoginAsync(login.LoginProvider, login.ProviderKey);
        if (existingUser != null)
        {
            Logger.LogWarning(4, "AddLogin for user failed because it was already associated with another user.");
            return IdentityResult.Failed(ErrorDescriber.LoginAlreadyAssociated());
        }

        await _userService.AddLoginAsync(user, login, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override Task<User> FindByLoginAsync(string loginProvider, string providerKey)
    {
        ThrowIfDisposed();
        if (loginProvider == null) throw new ArgumentNullException(nameof(loginProvider));

        if (providerKey == null) throw new ArgumentNullException(nameof(providerKey));

        return _userService.FindByLoginAsync(loginProvider, providerKey, CancellationToken);
    }

    public override async Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey)
    {
        ThrowIfDisposed();
        if (loginProvider == null) throw new ArgumentNullException(nameof(loginProvider));
        if (providerKey == null) throw new ArgumentNullException(nameof(providerKey));
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _userService.RemoveLoginAsync(user, loginProvider, providerKey, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return await UpdateUserAsync(user);
    }
}