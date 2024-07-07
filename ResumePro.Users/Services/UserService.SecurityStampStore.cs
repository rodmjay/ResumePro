#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        user.SecurityStamp = stamp;
        return Task.CompletedTask;
    }

    public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.SecurityStamp);
    }
}