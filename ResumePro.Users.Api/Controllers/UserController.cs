#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Users.Interfaces;
using TechValley.Rest.Bases;

namespace ResumePro.Users.Api.Controllers;

public class UserController(IServiceProvider serviceProvider, IUserService userService, IUserAccessor accessor)
    : BaseController(serviceProvider), IUserController
{
    [HttpGet]
    public async Task<UserOutput> GetUser()
    {
        var user = await GetCurrentUser();

        return await userService.GetUserById<UserOutput>(user.Id);
    }


    protected Task<UserOutput> GetCurrentUser()
    {
        return accessor.GetUser(User);
    }
}