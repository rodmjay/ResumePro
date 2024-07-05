#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

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

    public int OrganizationId
    {
        get
        {
            // Attempt to retrieve the organizationId claim as a string
            var organizationIdClaim = User.Claims.FirstOrDefault(c => c.Type == "organizationId")?.Value;

            // Try parsing the claim value to an integer
            if (int.TryParse(organizationIdClaim, out int organizationId))
            {
                return organizationId;
            }
            else
            {
                // Handle the case where the claim is missing or not an integer
                throw new Exception("The organizationId claim is missing or not a valid integer.");
            }
        }
    }
}