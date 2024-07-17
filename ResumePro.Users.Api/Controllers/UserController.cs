#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Users.Interfaces;
using ResumePro.Users.Services;

namespace ResumePro.Users.Api.Controllers;

public class UserController : BaseController, IUserController
{
    private readonly IUserService _userService;
    private readonly IUserAccessor _accessor;

    public UserController(IServiceProvider serviceProvider, IUserService userService, IUserAccessor accessor) : base(serviceProvider)
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