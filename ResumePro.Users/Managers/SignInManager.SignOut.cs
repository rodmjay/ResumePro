#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ResumePro.Users.Managers;

public partial class SignInManager
{
    public override async Task SignOutAsync()
    {
        await Context.SignOutAsync(Constants.LocalIdentity.DefaultApplicationScheme);
        await Context.SignOutAsync(Constants.LocalIdentity.DefaultExternalScheme);
        await Context.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
    }
}