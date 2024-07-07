#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<IdentityResult> ChangeEmailAsync(User user, string newEmail, string token)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        // Make sure the token is valid and the stamp matches
        if (!await VerifyUserTokenAsync(user, Options.Tokens.ChangeEmailTokenProvider,
                GetChangeEmailTokenPurpose(newEmail), token))
            return IdentityResult.Failed(ErrorDescriber.InvalidToken());
        await _userService.SetEmailAsync(user, newEmail, CancellationToken);
        await _userService.SetEmailConfirmedAsync(user, true, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return await UpdateUserAsync(user);
    }

    public override Task<User> FindByEmailAsync(string email)
    {
        ThrowIfDisposed();
        return _userService.FindByEmailAsync(email, CancellationToken.None);
    }

    public override async Task<string> GetEmailAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await _userService.GetEmailAsync(user, CancellationToken);
    }

    public override string NormalizeEmail(string email)
    {
        return KeyNormalizer == null ? email : KeyNormalizer.NormalizeEmail(email);
    }


    public override async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (!await VerifyUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider,
                ConfirmEmailTokenPurpose, token))
            return IdentityResult.Failed(ErrorDescriber.InvalidToken());
        await _userService.SetEmailConfirmedAsync(user, true, CancellationToken);
        return await UpdateUserAsync(user);
    }

    public override async Task<bool> IsEmailConfirmedAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        return await _userService.GetEmailConfirmedAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> SetEmailAsync(User user, string email)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _userService.SetEmailAsync(user, email, CancellationToken);
        await _userService.SetEmailConfirmedAsync(user, false, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return await UpdateUserAsync(user);
    }

    public override Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail)
    {
        ThrowIfDisposed();
        return GenerateUserTokenAsync(user, Options.Tokens.ChangeEmailTokenProvider,
            GetChangeEmailTokenPurpose(newEmail));
    }


    public override Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        ThrowIfDisposed();
        return GenerateUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider,
            ConfirmEmailTokenPurpose);
    }

    public override async Task UpdateNormalizedEmailAsync(User user)
    {
        ThrowIfDisposed();
        var email = await GetEmailAsync(user);
        await _userService.SetNormalizedEmailAsync(user, ProtectPersonalData(NormalizeEmail(email)),
            CancellationToken);
    }
}