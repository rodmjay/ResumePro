#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;
using ResumePro.Users.Interfaces;

namespace ResumePro.Users.Managers;

public partial class RoleManager : RoleManager<Role>
{
    private readonly IRoleService _roleService;

    public RoleManager(
        IRoleService roleService,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<RoleManager> logger) : base(roleService, roleValidators, keyNormalizer, errors, logger)
    {
        _roleService = roleService;
    }

    public override IQueryable<Role> Roles => _roleService.Roles;

    public override async Task<IdentityResult> CreateAsync(Role role)
    {
        ThrowIfDisposed();
        if (role == null)
            throw new ArgumentNullException(nameof(role));
        var result = await ValidateRoleAsync(role);
        if (!result.Succeeded)
            return result;
        await UpdateNormalizedRoleNameAsync(role);
        result = await _roleService.CreateAsync(role, CancellationToken);
        return result;
    }

    public override async Task UpdateNormalizedRoleNameAsync(Role role)
    {
        var name = await GetRoleNameAsync(role);
        await _roleService.SetNormalizedRoleNameAsync(role, NormalizeKey(name), CancellationToken);
    }

    public override Task<IdentityResult> UpdateAsync(Role role)
    {
        ThrowIfDisposed();
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        return UpdateRoleAsync(role);
    }

    public override Task<IdentityResult> DeleteAsync(Role role)
    {
        ThrowIfDisposed();
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        return _roleService.DeleteAsync(role, CancellationToken);
    }

    public override async Task<bool> RoleExistsAsync(string roleName)
    {
        ThrowIfDisposed();
        if (roleName == null)
            throw new ArgumentNullException(nameof(roleName));

        return await FindByNameAsync(NormalizeKey(roleName)) != null;
    }

    public override Task<Role> FindByIdAsync(string roleId)
    {
        ThrowIfDisposed();
        return _roleService.FindByIdAsync(roleId, CancellationToken);
    }

    public override Task<Role> FindByNameAsync(string roleName)
    {
        ThrowIfDisposed();
        if (roleName == null)
            throw new ArgumentNullException(nameof(roleName));

        return _roleService.FindByNameAsync(NormalizeKey(roleName), CancellationToken);
    }

    public override Task<string> GetRoleIdAsync(Role role)
    {
        ThrowIfDisposed();
        return _roleService.GetRoleIdAsync(role, CancellationToken);
    }

    public override Task<string> GetRoleNameAsync(Role role)
    {
        ThrowIfDisposed();
        return _roleService.GetRoleNameAsync(role, CancellationToken);
    }

    public override async Task<IdentityResult> SetRoleNameAsync(Role role, string name)
    {
        ThrowIfDisposed();

        await _roleService.SetRoleNameAsync(role, name, CancellationToken);
        await UpdateNormalizedRoleNameAsync(role);
        return IdentityResult.Success;
    }

    protected override async Task<IdentityResult> UpdateRoleAsync(Role role)
    {
        var result = await ValidateRoleAsync(role);
        if (!result.Succeeded)
            return result;
        await UpdateNormalizedRoleNameAsync(role);
        return await _roleService.UpdateAsync(role, CancellationToken);
    }

    protected override async Task<IdentityResult> ValidateRoleAsync(Role role)
    {
        var errors = new List<IdentityError>();
        foreach (var v in RoleValidators)
        {
            var result = await v.ValidateAsync(this, role);
            if (!result.Succeeded) errors.AddRange(result.Errors);
        }

        if (errors.Count > 0)
        {
            Logger.LogWarning(0, "Role {roleId} validation failed: {errors}.", await GetRoleIdAsync(role),
                string.Join(";", errors.Select(e => e.Code)));
            return IdentityResult.Failed(errors.ToArray());
        }

        return IdentityResult.Success;
    }
}