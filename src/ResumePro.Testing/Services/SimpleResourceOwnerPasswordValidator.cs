#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using ResumePro.Users.Interfaces;
using IdentityModel;

namespace ResumePro.Testing.Services;

public class SimpleResourceOwnerPasswordValidator : ILocalResourceOwnerPasswordValidator
{
    public Task ValidateAsync(LocalResourceOwnerPasswordValidationContext context)
    {
        if (context.UserName != "admin@admin.com" || context.Password != "ASDFasdf!")
        {
            context.Result =
                new LocalGrantValidationResult(LocalTokenRequestErrors.InvalidRequest, "Username or password is wrong!");
            return Task.CompletedTask;
        }

        context.Result = new LocalGrantValidationResult("1", OidcConstants.AuthenticationMethods.Password,
            new List<Claim>
            {
                new(JwtClaimTypes.Subject, "1"),
                new Claim("organizationId", "1")
            });

        return Task.CompletedTask;
    }
}
