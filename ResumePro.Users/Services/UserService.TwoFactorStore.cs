#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public async Task<bool> RedeemCodeAsync(User user, string code, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        if (user == null) throw new ArgumentNullException(nameof(user));
        if (code == null) throw new ArgumentNullException(nameof(code));

        var mergedCodes =
            await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken) ?? "";
        var splitCodes = mergedCodes.Split(';');
        if (splitCodes.Contains(code))
        {
            var updatedCodes = new List<string>(splitCodes.Where(s => s != code));
            await ReplaceCodesAsync(user, updatedCodes, cancellationToken);
            return true;
        }

        return false;
    }

    public virtual Task ReplaceCodesAsync(User user, IEnumerable<string> recoveryCodes,
        CancellationToken cancellationToken)
    {
        var mergedCodes = string.Join(";", recoveryCodes);
        return SetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, mergedCodes, cancellationToken);
    }

    public async Task<int> CountCodesAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        if (user == null) throw new ArgumentNullException(nameof(user));
        var mergedCodes =
            await GetTokenAsync(user, InternalLoginProvider, RecoveryCodeTokenName, cancellationToken) ?? "";
        if (mergedCodes.Length > 0) return mergedCodes.Split(';').Length;
        return 0;
    }

    public Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        user.TwoFactorEnabled = enabled;
        return Task.CompletedTask;
    }

    public Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();

        return Task.FromResult(user.TwoFactorEnabled);
    }
}