#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    protected override string CreateTwoFactorRecoveryCode()
    {
        return Guid.NewGuid().ToString().Substring(0, 8);
    }

    public override Task<int> CountRecoveryCodesAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return _userService.CountCodesAsync(user, CancellationToken);
    }

    public override async Task<IList<string>> GetValidTwoFactorProvidersAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var results = new List<string>();
        foreach (var f in _tokenProviders)
            if (await f.Value.CanGenerateTwoFactorTokenAsync(this, user))
                results.Add(f.Key);

        return results;
    }

    public override Task<string> GenerateTwoFactorTokenAsync(User user, string tokenProvider)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (!_tokenProviders.ContainsKey(tokenProvider))
            throw new NotSupportedException("NoTokenProvider");

        return _tokenProviders[tokenProvider].GenerateAsync("TwoFactor", this, user);
    }

    public override async Task<bool> GetTwoFactorEnabledAsync(User user)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return await _userService.GetTwoFactorEnabledAsync(user, CancellationToken);
    }

    public override async Task<IdentityResult> SetTwoFactorEnabledAsync(User user, bool enabled)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _userService
            .SetTwoFactorEnabledAsync(user, enabled, CancellationToken);

        await UpdateSecurityStampInternal(user);

        return await UpdateUserAsync(user);
    }

    public override async Task<bool> VerifyTwoFactorTokenAsync(User user, string tokenProvider, string token)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (!_tokenProviders.ContainsKey(tokenProvider))
            throw new NotSupportedException("NoTokenProvider");

        var result = await _tokenProviders[tokenProvider].ValidateAsync("TwoFactor", token, this, user);
        if (!result)
            _logger.LogWarning(10, $"{nameof(VerifyTwoFactorTokenAsync)}() failed for user.");

        return result;
    }

    public override async Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(User user, string code)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var success = await _userService.RedeemCodeAsync(user, code, CancellationToken);
        if (success)
            return await UpdateAsync(user);

        return IdentityResult.Failed(ErrorDescriber.RecoveryCodeRedemptionFailed());
    }

    public override async Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(User user, int number)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var newCodes = new List<string>(number);
        for (var i = 0; i < number; i++)
            newCodes.Add(CreateTwoFactorRecoveryCode());

        await _userService.ReplaceCodesAsync(user, newCodes.Distinct(), CancellationToken);

        var update = await UpdateAsync(user);

        if (update.Succeeded)
            return newCodes;

        return null;
    }
}