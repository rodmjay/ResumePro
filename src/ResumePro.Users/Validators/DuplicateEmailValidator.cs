#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Validators;

public class DuplicateEmailValidator : IUserValidator<User>
{
    private readonly IdentityErrorDescriber _errors;

    public DuplicateEmailValidator(IdentityErrorDescriber errors)
    {
        _errors = errors;
    }

    public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var userByEmail = await manager.FindByEmailAsync(user.NormalizedEmail);
        if (userByEmail != null) return IdentityResult.Failed(_errors.DuplicateEmail(user.Email));
        return IdentityResult.Success;
    }
}