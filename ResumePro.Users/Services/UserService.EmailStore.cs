#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.EntityFrameworkCore;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        user.Email = email;
        return UpdateAsync(user, cancellationToken);
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(User user, bool confirmed,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        user.EmailConfirmed = confirmed;
        return UpdateAsync(user, cancellationToken);
    }

    public Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Users.Where(x => x.NormalizedEmail == normalizedEmail).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.NormalizedEmail = normalizedEmail;
        return Task.CompletedTask;
    }
}