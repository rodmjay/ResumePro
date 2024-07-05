#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;

namespace ResumePro.Api.Controllers;

public class IdentityController : BaseController
{
    public IdentityController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    [HttpGet]
    public IActionResult GetIdentity()
    {
        return new JsonResult(from c in User.Claims select new {c.Type, c.Value});
    }
}