#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ResumePro.Core.Services.Bases;
using ResumePro.Shared.Models;
using ResumePro.Users.Entities;
using ResumePro.Users.Interfaces;
using ResumePro.Users.Managers;

namespace ResumePro.Users.Services;

public class UserAccessor : BaseService<User>, IUserAccessor
{
    private readonly UserManager _userManager;

    public UserAccessor(
        UserManager userManager,
        IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _userManager = userManager;
    }

    public Task<UserOutput> GetUser(ClaimsPrincipal principal)
    {
        var id = _userManager.GetUserId(principal);

        var userId = int.Parse(id);

        return _userManager.Users.Where(x => x.Id == userId)
            .ProjectTo<UserOutput>(ProjectionMapping)
            .FirstOrDefaultAsync();
    }
}