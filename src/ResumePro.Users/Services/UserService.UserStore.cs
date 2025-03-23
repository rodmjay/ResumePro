#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public IQueryable<User> Users => Repository.Queryable()
        .Include(x => x.UserRoles)
        .Include(x => x.UserTokens);

    public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.Id.ToString());
    }

    public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(User user, string userName,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        user.UserName = userName;
        return UpdateAsync(user, cancellationToken);
    }

    public Task<string> GetNormalizedUserNameAsync(User user,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(User user, string normalizedName,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    public Task<IdentityResult> CreateAsync(User user,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var errors = new List<IdentityError>();

        user.ObjectState = ObjectState.Added;

        Repository.Attach(user);

        var changes = Repository.Commit();
        if (changes > 0)
            return Task.FromResult(IdentityResult.Success);

        errors.Add(_errors.DefaultError());

        return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
    }

    public async Task<IdentityResult> UpdateAsync(User user,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.ObjectState = ObjectState.Modified;
        user.ConcurrencyStamp = Guid.NewGuid().ToString();
        try
        {
            await Repository.UpdateAsync(user, true);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(_errors.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        user.ObjectState = ObjectState.Deleted;
        try
        {
            await Repository.DeleteAsync(user, true);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(_errors.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    public Task<User> FindByIdAsync(string userIdString, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        var id = ConvertIdFromString(userIdString);
        return Repository.Queryable().Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Repository.Queryable().Where(x => x.NormalizedUserName == normalizedUserName)
            .FirstOrDefaultAsync(cancellationToken);
    }
}