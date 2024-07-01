#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ResumePro.Core.Settings;

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
}