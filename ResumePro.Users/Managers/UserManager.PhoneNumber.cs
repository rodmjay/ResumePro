#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    public override async Task<string> GetPhoneNumberAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        return await _userService.GetPhoneNumberAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> ChangePhoneNumberAsync(User user, string phoneNumber, string token)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (!await VerifyChangePhoneNumberTokenAsync(user, token, phoneNumber))
        {
            Logger.LogWarning(GetLogMessage("Change phone number for user failed with invalid token."));
            return IdentityResult.Failed(ErrorDescriber.InvalidToken());
        }

        await _userService.SetPhoneNumberAsync(user, phoneNumber, CancellationToken);
        await _userService.SetPhoneNumberConfirmedAsync(user, true, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return await UpdateUserAsync(user);
    }

    public override Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber)
    {
        ThrowIfDisposed();
        return GenerateUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider,
            ChangePhoneNumberTokenPurpose + ":" + phoneNumber);
    }

    public override Task<bool> IsPhoneNumberConfirmedAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        return _userService.GetPhoneNumberConfirmedAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        await _userService.SetPhoneNumberAsync(user, phoneNumber, CancellationToken);
        await _userService.SetPhoneNumberConfirmedAsync(user, false, CancellationToken);
        await UpdateSecurityStampInternal(user);
        return await UpdateUserAsync(user);
    }

    public override Task<bool> VerifyChangePhoneNumberTokenAsync(User user, string token, string phoneNumber)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        // Make sure the token is valid and the stamp matches
        return VerifyUserTokenAsync(user, Options.Tokens.ChangePhoneNumberTokenProvider,
            ChangePhoneNumberTokenPurpose + ":" + phoneNumber, token);
    }
}