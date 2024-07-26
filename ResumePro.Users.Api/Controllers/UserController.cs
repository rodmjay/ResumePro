#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Users.Interfaces;

namespace ResumePro.Users.Api.Controllers;

public class UserController : BaseController, IUserController
{
    private readonly IUserAccessor _accessor;
    private readonly IUserService _userService;

    public UserController(IServiceProvider serviceProvider, IUserService userService, IUserAccessor accessor) : base(
        serviceProvider)
    {
        _userService = userService;
        _accessor = accessor;
    }

    [HttpGet]
    public async Task<UserOutput> GetUser()
    {
        var user = await GetCurrentUser();

        return await _userService.GetUserById<UserOutput>(user.Id);
    }


    protected Task<UserOutput> GetCurrentUser()
    {
        return _accessor.GetUser(User);
    }
}