#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumePro.Users.Entities;
using ResumePro.Users.Interfaces;

namespace ResumePro.Users.Managers;

public partial class UserManager : UserManager<User>
{
    private readonly ILogger<UserManager> _logger;
    private readonly IServiceProvider _services;
    private readonly IUserService _userService;

    public UserManager(
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        IUserService userService,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager> logger) : base(userService, optionsAccessor, passwordHasher, userValidators,
        passwordValidators, keyNormalizer, errors, services, logger)
    {
        _userService = userService;
        _services = services;
        _logger = logger;
    }


    public override IQueryable<User> Users => _userService.Users;

    private string GetLogMessage(string message, [CallerMemberName] string callerName = null)
    {
        return $"[{nameof(UserManager)}.{callerName}] - {message}";
    }

    public override Task<User> FindByIdAsync(string userId)
    {
        ThrowIfDisposed();
        return _userService.FindByIdAsync(userId, CancellationToken.None);
    }

    public override async Task<User> FindByNameAsync(string userName)
    {
        ThrowIfDisposed();
        if (userName == null) throw new ArgumentNullException(nameof(userName));
        userName = NormalizeName(userName);

        var user = await _userService.FindByNameAsync(userName, CancellationToken.None);

        // data protection stuff would go here

        return user;
    }

    public override Task<User> GetUserAsync(ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentNullException(nameof(principal));
        var id = GetUserId(principal);
        return id == null ? Task.FromResult<User>(null) : FindByIdAsync(id);
    }

    public override Task<IdentityResult> DeleteAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return _userService.DeleteAsync(user, CancellationToken);
    }

    public override string GetUserId(ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentNullException(nameof(principal));
        return principal.FindFirstValue(Options.ClaimsIdentity.UserIdClaimType);
    }

    public override Task<IdentityResult> UpdateAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return UpdateUserAsync(user);
    }

    public override async Task<string> GetUserNameAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return await _userService.GetUserNameAsync(user, CancellationToken);
    }

    private async Task<IdentityResult> ValidateUser(User user)
    {
        if (SupportsUserSecurityStamp)
        {
            var stamp = await GetSecurityStampAsync(user);
            if (stamp == null) throw new InvalidOperationException("NullSecurityStamp");
        }

        var errors = new List<IdentityError>();
        foreach (var v in UserValidators)
        {
            var result = await v.ValidateAsync(this, user);
            if (!result.Succeeded) errors.AddRange(result.Errors);
        }

        if (errors.Count > 0)
        {
            _logger.LogWarning(GetLogMessage("User validation failed: {errors}."),
                string.Join(";", errors.Select(e => e.Code)));
            return IdentityResult.Failed(errors.ToArray());
        }

        return IdentityResult.Success;
    }

    public override async Task<IdentityResult> SetUserNameAsync(User user, string userName)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _userService.SetUserNameAsync(user, userName, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return await UpdateUserAsync(user);
    }

    protected override async Task<IdentityResult> UpdateUserAsync(User user)
    {
        var result = await ValidateUserAsync(user);
        if (!result.Succeeded) return result;
        await UpdateNormalizedUserNameAsync(user);
        await UpdateNormalizedEmailAsync(user);
        return await _userService.UpdateAsync(user, CancellationToken);
    }

    public override string NormalizeName(string name)
    {
        return KeyNormalizer == null ? name : KeyNormalizer.NormalizeName(name);
    }

    public override string GetUserName(ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentNullException(nameof(principal));
        return principal.FindFirstValue(Options.ClaimsIdentity.UserNameClaimType);
    }

    public override async Task<string> GetUserIdAsync(User user)
    {
        ThrowIfDisposed();
        return await _userService.GetUserIdAsync(user, CancellationToken);
    }


    private string ProtectPersonalData(string data)
    {
        if (Options.Stores.ProtectPersonalData)
        {
            var keyRing = _services.GetService<ILookupProtectorKeyRing>();
            var protector = _services.GetService<ILookupProtector>();
            return protector.Protect(keyRing.CurrentKeyId, data);
        }

        return data;
    }

    public override async Task UpdateNormalizedUserNameAsync(User user)
    {
        var normalizedName = NormalizeName(await GetUserNameAsync(user));
        normalizedName = ProtectPersonalData(normalizedName);
        await Store.SetNormalizedUserNameAsync(user, normalizedName, CancellationToken);
    }
}