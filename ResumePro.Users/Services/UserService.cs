#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.ComponentModel;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Shared.Models;
using ResumePro.Users.Entities;
using ResumePro.Users.Interfaces;

namespace ResumePro.Users.Services;

public partial class UserService : BaseService<User>, IUserService
{
    private const string InternalLoginProvider = "[AspNetUserStore]";
    private const string AuthenticatorKeyTokenName = "AuthenticatorKey";
    private const string RecoveryCodeTokenName = "RecoveryCodes";
    private readonly IdentityErrorDescriber _errors;
    private readonly IRepositoryAsync<Role> _roleRepository;
    private readonly IRepositoryAsync<UserClaim> _userClaimsRepository;
    private readonly IRepositoryAsync<UserLogin> _userLoginRepository;
    private readonly IRepositoryAsync<UserRole> _userRoleRepository;

    private readonly IRepositoryAsync<UserToken> _userTokenRepository;
    private bool _disposed;

    public UserService(
        IdentityErrorDescriber errors,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userTokenRepository = UnitOfWork.RepositoryAsync<UserToken>();
        _roleRepository = UnitOfWork.RepositoryAsync<Role>();
        _userRoleRepository = UnitOfWork.RepositoryAsync<UserRole>();
        _userClaimsRepository = UnitOfWork.RepositoryAsync<UserClaim>();
        _userLoginRepository = UnitOfWork.RepositoryAsync<UserLogin>();

        _errors = errors;
    }


    public void Dispose()
    {
        UnitOfWork.Dispose();
        _disposed = true;
    }

    public Task<T> GetUserById<T>(int id) where T : UserOutput
    {
        return Users.Where(x => x.Id == id).ProjectTo<T>(ProjectionMapping).FirstAsync();
    }

    private Task<User> FindUserAsync(int userId, CancellationToken cancellationToken)
    {
        return Users.SingleOrDefaultAsync(u => u.Id.Equals(userId), cancellationToken);
    }

    private int ConvertIdFromString(string id)
    {
        if (id == null) return default;
        return (int) TypeDescriptor.GetConverter(typeof(int)).ConvertFromInvariantString(id);
    }

    protected void ThrowIfDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(GetType().Name);
    }
}