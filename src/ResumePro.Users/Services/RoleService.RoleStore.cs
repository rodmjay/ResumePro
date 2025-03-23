#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class RoleService
{
    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));
        return Task.FromResult(ConvertIdToString(role.Id));
    }

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));
        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));
        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));
        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    public Task<Role> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        var roleId = ConvertIdFromString(id);
        return Roles.FirstOrDefaultAsync(u => u.Id.Equals(roleId), cancellationToken);
    }

    public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        return Roles.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
    }

    public Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        if (role == null)
            throw new ArgumentNullException(nameof(role));

        Repository.Attach(role);
        Repository.Commit();

        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));

        Repository.Attach(role);
        role.ConcurrencyStamp = Guid.NewGuid().ToString();

        try
        {
            await Repository.UpdateAsync(role);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(_errors.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (role == null) throw new ArgumentNullException(nameof(role));

        try
        {
            await Repository.DeleteAsync(role);
        }
        catch (DbUpdateConcurrencyException)
        {
            return IdentityResult.Failed(_errors.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }
}