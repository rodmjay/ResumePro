#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Security.Claims;
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

    public int UserId
    {
        get
        {
            // Attempt to retrieve the organizationId claim as a string
            var userIdClaim = User.Identity.Name;

            // Try parsing the claim value to an integer
                if (int.TryParse(userIdClaim, out var userId))
                return userId;
            throw new Exception("The userId claim is missing or not a valid integer.");
        }
    }

    public int OrganizationId
    {
        get
        {
            // Attempt to retrieve the organizationId claim as a string
            var organizationIdClaim = User.Claims.FirstOrDefault(c => c.Type == "organizationId")?.Value;

            // Try parsing the claim value to an integer
            if (int.TryParse(organizationIdClaim, out var organizationId))
                return organizationId;
            throw new Exception("The organizationId claim is missing or not a valid integer.");
        }
    }
}