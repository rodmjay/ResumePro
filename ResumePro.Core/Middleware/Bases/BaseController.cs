#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResumePro.Core.Extensions;
using ResumePro.Core.Settings;
using ResumePro.Shared.Extensions;

namespace ResumePro.Core.Middleware.Bases;

[ApiController]
[Produces("application/json")]
[Route("v1.0/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly AppSettings AppSettings;

    /// <param name="serviceProvider"></param>
    protected BaseController(IServiceProvider serviceProvider)
    {
        AppSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
    }

    public int UserId => User.UserId();

    public int OrganizationId => User.OrganizationId();
}