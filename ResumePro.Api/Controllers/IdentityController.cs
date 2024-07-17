#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;

namespace ResumePro.Api.Controllers;

public sealed class IdentityController(IServiceProvider serviceProvider) : BaseController(serviceProvider)
{
    [HttpGet]
    public IActionResult GetIdentity()
    {
        return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
    }
}