#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class UserManager
{
    private readonly Dictionary<string, IUserTwoFactorTokenProvider<User>> _tokenProviders =
        new();

    public override Task<string> GenerateUserTokenAsync(User user, string tokenProvider, string purpose)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (tokenProvider == null)
            throw new ArgumentNullException(nameof(tokenProvider));

        if (!_tokenProviders.ContainsKey(tokenProvider))
            throw new NotSupportedException("NoTokenProvider");

        return _tokenProviders[tokenProvider].GenerateAsync(purpose, this, user);
    }

    public override void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<User> provider)
    {
        ThrowIfDisposed();

        _tokenProviders[providerName] = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public override async Task<bool> VerifyUserTokenAsync(User user, string tokenProvider, string purpose,
        string token)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (tokenProvider == null)
            throw new ArgumentNullException(nameof(tokenProvider));

        if (!_tokenProviders.ContainsKey(tokenProvider))
            throw new NotSupportedException("NoTokenProviderForUser");

        var result = await _tokenProviders[tokenProvider].ValidateAsync(purpose, token, this, user);

        if (!result)
            _logger.LogWarning(9, "VerifyUserTokenAsync() failed with purpose: {purpose} for user.", purpose);

        return result;
    }

    public override async Task<byte[]> CreateSecurityTokenAsync(User user)
    {
        return Encoding.Unicode.GetBytes(await GetSecurityStampAsync(user));
    }


    public override async Task<IdentityResult> RemoveAuthenticationTokenAsync(User user, string loginProvider,
        string tokenName)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (loginProvider == null) throw new ArgumentNullException(nameof(loginProvider));
        if (tokenName == null) throw new ArgumentNullException(nameof(tokenName));

        await _userService.RemoveTokenAsync(user, loginProvider, tokenName, CancellationToken);
        return await UpdateUserAsync(user);
    }


    public override Task<string> GetAuthenticationTokenAsync(User user, string loginProvider, string tokenName)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (loginProvider == null) throw new ArgumentNullException(nameof(loginProvider));
        if (tokenName == null) throw new ArgumentNullException(nameof(tokenName));

        return _userService.GetTokenAsync(user, loginProvider, tokenName, CancellationToken);
    }


    public override async Task<IdentityResult> SetAuthenticationTokenAsync(User user, string loginProvider,
        string tokenName, string tokenValue)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        if (loginProvider == null)
            throw new ArgumentNullException(nameof(loginProvider));
        if (tokenName == null)
            throw new ArgumentNullException(nameof(tokenName));

        // REVIEW: should updating any tokens affect the security stamp?
        await _userService.SetTokenAsync(user, loginProvider, tokenName, tokenValue, CancellationToken);
        return await UpdateUserAsync(user);
    }
}