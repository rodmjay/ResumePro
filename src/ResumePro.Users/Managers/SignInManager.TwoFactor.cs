#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class SignInManager
{
    public override async Task<User> GetTwoFactorAuthenticationUserAsync()
    {
        var info = await RetrieveTwoFactorInfoAsync();
        if (info == null) return null;

        return await UserManager.FindByIdAsync(info.UserId);
    }

    public override Task ForgetTwoFactorClientAsync()
    {
        return Context.SignOutAsync(IdentityConstants.TwoFactorRememberMeScheme);
    }

    internal ClaimsPrincipal StoreTwoFactorInfo(string userId, string loginProvider)
    {
        var identity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, userId));
        if (loginProvider != null) identity.AddClaim(new Claim(ClaimTypes.AuthenticationMethod, loginProvider));
        return new ClaimsPrincipal(identity);
    }

    protected override async Task<SignInResult> SignInOrTwoFactorAsync(User user, bool isPersistent,
        string loginProvider = null, bool bypassTwoFactor = false)
    {
        if (!bypassTwoFactor && await IsTfaEnabled(user))
            if (!await IsTwoFactorClientRememberedAsync(user))
            {
                // Store the userId for use after two factor check
                var userId = await UserManager.GetUserIdAsync(user);
                await Context.SignInAsync(IdentityConstants.TwoFactorUserIdScheme,
                    StoreTwoFactorInfo(userId, loginProvider));
                return SignInResult.TwoFactorRequired;
            }

        // Cleanup external cookie
        if (loginProvider != null) await Context.SignOutAsync(IdentityConstants.ExternalScheme);
        if (loginProvider == null)
            await SignInWithClaimsAsync(user, isPersistent, new[] {new Claim("amr", "pwd")});
        else
            await SignInAsync(user, isPersistent, loginProvider);
        return SignInResult.Success;
    }

    private async Task<bool> IsTfaEnabled(User user)
    {
        return UserManager.SupportsUserTwoFactor &&
               await UserManager.GetTwoFactorEnabledAsync(user) &&
               (await UserManager.GetValidTwoFactorProvidersAsync(user)).Count > 0;
    }

    public override async Task<User> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal)
    {
        if (principal == null || principal.Identity?.Name == null) return null;

        var user = await UserManager.FindByIdAsync(principal.Identity.Name);
        if (await ValidateSecurityStampAsync(user,
                principal.FindFirstValue(Options.ClaimsIdentity.SecurityStampClaimType)))
            return user;

        Logger.LogDebug(5, "Failed to validate a security stamp.");
        return null;
    }

    public override async Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent,
        bool rememberClient)
    {
        var twoFactorInfo = await RetrieveTwoFactorInfoAsync();
        if (twoFactorInfo == null || twoFactorInfo.UserId == null) return SignInResult.Failed;
        var user = await UserManager.FindByIdAsync(twoFactorInfo.UserId);
        if (user == null) return SignInResult.Failed;

        var error = await PreSignInCheck(user);
        if (error != null) return error;

        if (await UserManager.VerifyTwoFactorTokenAsync(user, Options.Tokens.AuthenticatorTokenProvider, code))
        {
            await DoTwoFactorSignInAsync(user, twoFactorInfo, isPersistent, rememberClient);
            return SignInResult.Success;
        }

        // If the token is incorrect, record the failure which also may cause the user to be locked out
        await UserManager.AccessFailedAsync(user);
        return SignInResult.Failed;
    }


    private async Task DoTwoFactorSignInAsync(User user, TwoFactorAuthenticationInfo twoFactorInfo,
        bool isPersistent, bool rememberClient)
    {
        // When token is verified correctly, clear the access failed count used for lockout
        await ResetLockout(user);

        var claims = new List<Claim> {new("amr", "mfa")};

        // Cleanup external cookie
        if (twoFactorInfo.LoginProvider != null)
        {
            claims.Add(new Claim(ClaimTypes.AuthenticationMethod, twoFactorInfo.LoginProvider));
            await Context.SignOutAsync(IdentityConstants.ExternalScheme);
        }

        // Cleanup two factor user id cookie
        await Context.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
        if (rememberClient) await RememberTwoFactorClientAsync(user);
        await SignInWithClaimsAsync(user, isPersistent, claims);
    }

    public override async Task RememberTwoFactorClientAsync(User user)
    {
        var principal = await StoreRememberClient(user);
        await Context.SignInAsync(IdentityConstants.TwoFactorRememberMeScheme,
            principal,
            new AuthenticationProperties {IsPersistent = true});
    }

    internal async Task<ClaimsPrincipal> StoreRememberClient(User user)
    {
        var userId = await UserManager.GetUserIdAsync(user);
        var rememberBrowserIdentity = new ClaimsIdentity(IdentityConstants.TwoFactorRememberMeScheme);
        rememberBrowserIdentity.AddClaim(new Claim(ClaimTypes.Name, userId));
        if (UserManager.SupportsUserSecurityStamp)
        {
            var stamp = await UserManager.GetSecurityStampAsync(user);
            rememberBrowserIdentity.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType, stamp));
        }

        return new ClaimsPrincipal(rememberBrowserIdentity);
    }

    public override async Task<bool> IsTwoFactorClientRememberedAsync(User user)
    {
        var userId = await UserManager.GetUserIdAsync(user);
        var result = await Context.AuthenticateAsync(IdentityConstants.TwoFactorRememberMeScheme);
        return result?.Principal != null && result.Principal.FindFirstValue(ClaimTypes.Name) == userId;
    }

    public override async Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent,
        bool rememberClient)
    {
        var twoFactorInfo = await RetrieveTwoFactorInfoAsync();
        if (twoFactorInfo == null || twoFactorInfo.UserId == null) return SignInResult.Failed;
        var user = await UserManager.FindByIdAsync(twoFactorInfo.UserId);
        if (user == null) return SignInResult.Failed;

        var error = await PreSignInCheck(user);
        if (error != null) return error;
        if (await UserManager.VerifyTwoFactorTokenAsync(user, provider, code))
        {
            await DoTwoFactorSignInAsync(user, twoFactorInfo, isPersistent, rememberClient);
            return SignInResult.Success;
        }

        // If the token is incorrect, record the failure which also may cause the user to be locked out
        await UserManager.AccessFailedAsync(user);
        return SignInResult.Failed;
    }

    public override async Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode)
    {
        var twoFactorInfo = await RetrieveTwoFactorInfoAsync();
        if (twoFactorInfo == null || twoFactorInfo.UserId == null) return SignInResult.Failed;
        var user = await UserManager.FindByIdAsync(twoFactorInfo.UserId);
        if (user == null) return SignInResult.Failed;

        var result = await UserManager.RedeemTwoFactorRecoveryCodeAsync(user, recoveryCode);
        if (result.Succeeded)
        {
            await DoTwoFactorSignInAsync(user, twoFactorInfo, false, false);
            return SignInResult.Success;
        }

        // We don't protect against brute force attacks since codes are expected to be random.
        return SignInResult.Failed;
    }

    private async Task<TwoFactorAuthenticationInfo> RetrieveTwoFactorInfoAsync()
    {
        var result = await Context.AuthenticateAsync(IdentityConstants.TwoFactorUserIdScheme);
        if (result?.Principal != null)
            return new TwoFactorAuthenticationInfo
            {
                UserId = result.Principal.FindFirstValue(ClaimTypes.Name),
                LoginProvider = result.Principal.FindFirstValue(ClaimTypes.AuthenticationMethod)
            };
        return null;
    }


    internal class TwoFactorAuthenticationInfo
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
    }
}