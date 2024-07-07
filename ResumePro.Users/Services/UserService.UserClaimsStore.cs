#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using ResumePro.Users.Entities;

namespace ResumePro.Users.Services;

public partial class UserService
{
    public IQueryable<UserClaim> UserClaims => _userClaimsRepository.Queryable();

    public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        return await UserClaims.Where(uc => uc.UserId.Equals(user.Id))
            .Select(c => c.ToClaim()).ToListAsync(cancellationToken);
    }

    public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (claims == null) throw new ArgumentNullException(nameof(claims));
        foreach (var claim in claims)
        {
            var userClaim = CreateUserClaim(user, claim);
            _userClaimsRepository.InsertOrUpdateGraph(userClaim, true);
        }

        return Task.FromResult(false);
    }

    public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (claims == null) throw new ArgumentNullException(nameof(claims));
        foreach (var claim in claims)
        {
            var matchedClaims = await UserClaims
                .Where(uc =>
                    uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
                .ToListAsync(cancellationToken);
            _userClaimsRepository.DeleteMany(matchedClaims);
        }
    }


    public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ThrowIfDisposed();
        if (claim == null) throw new ArgumentNullException(nameof(claim));

        var query = from userclaims in UserClaims
            join user in Users on userclaims.UserId equals user.Id
            where userclaims.ClaimValue == claim.Value
                  && userclaims.ClaimType == claim.Type
            select user;

        return await query.ToListAsync(cancellationToken);
    }

    public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        if (user == null) throw new ArgumentNullException(nameof(user));

        if (claim == null) throw new ArgumentNullException(nameof(claim));

        if (newClaim == null) throw new ArgumentNullException(nameof(newClaim));

        var matchedClaims = await UserClaims
            .Where(uc => uc.UserId.Equals(user.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type)
            .ToListAsync(cancellationToken);
        foreach (var matchedClaim in matchedClaims)
        {
            matchedClaim.ClaimValue = newClaim.Value;
            matchedClaim.ClaimType = newClaim.Type;
        }
    }

    private UserClaim CreateUserClaim(User user, Claim claim)
    {
        var userClaim = new UserClaim
        {
            ObjectState = ObjectState.Added,
            UserId = user.Id
        };
        userClaim.InitializeFromClaim(claim);
        return userClaim;
    }
}