#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class SignInManager
{
    public override async Task<SignInResult> PasswordSignInAsync(User user, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var attempt = await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        return attempt.Succeeded
            ? await SignInOrTwoFactorAsync(user, isPersistent)
            : attempt;
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password,
        bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userName);
        if (user == null)
            return SignInResult.Failed;
        return await CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public override async Task<SignInResult> CheckPasswordSignInAsync(User user, string password,
        bool lockoutOnFailure)
    {
        var retVal = new SignInResult();

        var verification = VerifyPassword(user, password);
        var task = await UserManager.UpdateAsync(user);

        retVal = verification switch
        {
            PasswordVerificationResult.Failed => SignInResult.NotAllowed,
            PasswordVerificationResult.Success => await base.SignInOrTwoFactorAsync(user, false),
            _ => retVal
        };

        return retVal;
    }

    protected PasswordVerificationResult VerifyPassword(User account, string password)
    {
        _logger.LogDebug(GetLogMessage("Verifying Password: {UserName}"), account.UserName);
        return _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, password);
    }
}