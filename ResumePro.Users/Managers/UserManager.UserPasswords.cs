#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<bool> CheckPasswordAsync(User user, string password)
    {
        ThrowIfDisposed();
        if (user == null) return false;

        var result = await VerifyPasswordAsync(_userService, user, password);
        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            await UpdatePasswordHash(user, password, false);
            await UpdateUserAsync(user);
        }

        var success = result != PasswordVerificationResult.Failed;
        if (!success) Logger.LogWarning(0, "Invalid password for user.");
        return success;
    }

    public override async Task<IdentityResult> RemovePasswordAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        await UpdatePasswordHash(user, null, false);
        return await UpdateUserAsync(user);
    }

    public override async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword,
        string newPassword)
    {
        _logger.LogDebug(GetLogMessage("Changing Password: {UserName}"), user.UserName);

        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));


        if (await VerifyPasswordAsync(_userService, user, currentPassword) != PasswordVerificationResult.Failed)
        {
            var result = await UpdatePasswordHash(user, newPassword, true);
            if (!result.Succeeded) return result;

            return await UpdateUserAsync(user);
        }

        _logger.LogWarning(2, "Change password failed for user.");
        return IdentityResult.Failed(ErrorDescriber.PasswordMismatch());
    }

    public override Task<bool> HasPasswordAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return _userService.HasPasswordAsync(user, CancellationToken);
    }

    protected override async Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<User> store,
        User user, string password)
    {
        var hash = await _userService.GetPasswordHashAsync(user, CancellationToken);
        if (hash == null) return PasswordVerificationResult.Failed;

        return PasswordHasher.VerifyHashedPassword(user, hash, password);
    }

    public override async Task<IdentityResult> AddPasswordAsync(User user, string password)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        var hash = await _userService.GetPasswordHashAsync(user, CancellationToken);
        if (hash != null)
        {
            _logger.LogWarning(1, "User already has a password.");
            return IdentityResult.Failed(ErrorDescriber.UserAlreadyHasPassword());
        }

        var result = await UpdatePasswordHash(user, password, true);
        if (!result.Succeeded) return result;

        return await UpdateUserAsync(user);
    }

    protected override async Task<IdentityResult> UpdatePasswordHash(User user, string newPassword,
        bool validatePassword)
    {
        if (validatePassword)
        {
            var validate = await ValidatePasswordAsync(user, newPassword);
            if (!validate.Succeeded) return validate;
        }

        var hash = newPassword != null ? PasswordHasher.HashPassword(user, newPassword) : null;
        await _userService.SetPasswordHashAsync(user, hash, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return IdentityResult.Success;
    }

    public override Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        ThrowIfDisposed();
        return GenerateUserTokenAsync(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose);
    }


    public override async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        // Make sure the token is valid and the stamp matches
        if (!await VerifyUserTokenAsync(user, Options.Tokens.PasswordResetTokenProvider, ResetPasswordTokenPurpose,
                token)) return IdentityResult.Failed(ErrorDescriber.InvalidToken());
        var result = await UpdatePasswordHash(user, newPassword, true);
        if (!result.Succeeded) return result;
        return await UpdateUserAsync(user);
    }
}