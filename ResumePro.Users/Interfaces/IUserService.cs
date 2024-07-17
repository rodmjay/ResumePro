#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Identity;
using ResumePro.Core.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Interfaces;

public interface IUserService : IService<User>,
    IQueryableUserStore<User>,
    IUserPasswordStore<User>,
    IUserRoleStore<User>,
    IUserClaimStore<User>,
    IUserLoginStore<User>,
    IUserLockoutStore<User>,
    IUserPhoneNumberStore<User>,
    IUserEmailStore<User>,
    IUserAuthenticatorKeyStore<User>,
    IUserTwoFactorStore<User>,
    IUserTwoFactorRecoveryCodeStore<User>,
    IUserSecurityStampStore<User>,
    IUserAuthenticationTokenStore<User>
{
    Task<T> GetUserById<T>(int id) where T : UserOutput;
}