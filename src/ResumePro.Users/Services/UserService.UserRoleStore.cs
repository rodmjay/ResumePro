#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public IQueryable<UserRole> UserRoles => _userRoleRepository.Queryable();
    public IQueryable<Role> Roles => _roleRepository.Queryable();

    public async Task AddToRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(normalizedRoleName))
            throw new ArgumentException("ValueCannotBeNullOrEmpty", nameof(normalizedRoleName));

        var roleEntity = await FindRoleAsync(normalizedRoleName, cancellationToken);
        if (roleEntity == null)
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "RoleNotFound",
                normalizedRoleName));

        await _userRoleRepository.InsertAsync(CreateUserRole(user, roleEntity), true);
    }

    public async Task RemoveFromRoleAsync(User user, string normalizedRoleName,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(normalizedRoleName))
            throw new ArgumentException("ValueCannotBeNullOrEmpty", nameof(normalizedRoleName));

        var roleEntity = await FindRoleAsync(normalizedRoleName, cancellationToken);
        if (roleEntity != null)
        {
            var userRole = await FindUserRoleAsync(user.Id, roleEntity.Id, cancellationToken);
            if (userRole != null) await _userRoleRepository.DeleteAsync(userRole, true);
        }
    }

    public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var userId = user.Id;
        var query = from userRole in UserRoles
            join role in Roles on userRole.RoleId equals role.Id
            where userRole.UserId.Equals(userId)
            select role.Name;

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> IsInRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (string.IsNullOrWhiteSpace(normalizedRoleName))
            throw new ArgumentException("ValueCannotBeNullOrEmpty", nameof(normalizedRoleName));

        var role = await FindRoleAsync(normalizedRoleName, cancellationToken);
        if (role != null)
        {
            var userRole = await FindUserRoleAsync(user.Id, role.Id, cancellationToken);
            return userRole != null;
        }

        return false;
    }

    public async Task<IList<User>> GetUsersInRoleAsync(string normalizedRoleName,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (string.IsNullOrEmpty(normalizedRoleName)) throw new ArgumentNullException(nameof(normalizedRoleName));

        var role = await FindRoleAsync(normalizedRoleName, cancellationToken);

        if (role != null)
        {
            var query = from userrole in UserRoles
                join user in Users on userrole.UserId equals user.Id
                where userrole.RoleId.Equals(role.Id)
                select user;

            return await query.ToListAsync(cancellationToken);
        }

        return new List<User>();
    }

    protected virtual UserRole CreateUserRole(User user, Role role)
    {
        return new UserRole
        {
            ObjectState = ObjectState.Added,
            UserId = user.Id,
            RoleId = role.Id
        };
    }

    protected Task<UserRole> FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        return UserRoles.Where(x => x.UserId == userId && x.RoleId == roleId)
            .FirstOrDefaultAsync(cancellationToken);
    }

    private Task<Role> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return _roleRepository.Queryable()
            .SingleOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
    }
}