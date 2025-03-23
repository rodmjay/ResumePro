#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Managers;

public partial class SignInManager
{
    protected override Task<SignInResult> LockedOut(User user)
    {
        Logger.LogWarning(3, "User is currently locked out.");
        return Task.FromResult(SignInResult.LockedOut);
    }

    protected override async Task<bool> IsLockedOut(User user)
    {
        return UserManager.SupportsUserLockout && await UserManager.IsLockedOutAsync(user);
    }
}