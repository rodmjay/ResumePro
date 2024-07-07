#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public IQueryable<UserLogin> UserLogins => _userLoginRepository.Queryable();

    public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (login == null) throw new ArgumentNullException(nameof(login));

        _userLoginRepository.InsertAsync(CreateUserLogin(user, login), true);

        return Task.FromResult(false);
    }

    public async Task RemoveLoginAsync(User user, string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        var entry = await FindUserLoginAsync(user.Id, loginProvider, providerKey, cancellationToken);
        if (entry != null) _userLoginRepository.Delete(entry, true);
    }

    public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        var userId = user.Id;
        return await UserLogins.Where(l => l.UserId.Equals(userId))
            .Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName))
            .ToListAsync(cancellationToken);
    }

    public async Task<User> FindByLoginAsync(string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        var userLogin = await FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
        if (userLogin != null) return await FindUserAsync(userLogin.UserId, cancellationToken);
        return null;
    }

    private UserLogin CreateUserLogin(User user, UserLoginInfo login)
    {
        return new UserLogin
        {
            ObjectState = ObjectState.Added,
            UserId = user.Id,
            ProviderKey = login.ProviderKey,
            LoginProvider = login.LoginProvider,
            ProviderDisplayName = login.ProviderDisplayName
        };
    }

    protected Task<UserLogin> FindUserLoginAsync(string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        return UserLogins.SingleOrDefaultAsync(
            userLogin => userLogin.LoginProvider == loginProvider && userLogin.ProviderKey == providerKey,
            cancellationToken);
    }

    protected Task<UserLogin> FindUserLoginAsync(int userId, string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        return UserLogins.SingleOrDefaultAsync(
            userLogin => userLogin.UserId.Equals(userId) && userLogin.LoginProvider == loginProvider &&
                         userLogin.ProviderKey == providerKey, cancellationToken);
    }
}