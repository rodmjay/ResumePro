#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.AI.Services;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.OpenAI.Api.Controllers;

public class TextController(IServiceProvider serviceProvider, IChatGptService service)
    : BaseController(serviceProvider), ITextController
{
    [HttpPost]
    public async Task<ChatResult> Professionalize([FromBody] ChatOptions options)
    {
        var response = await service.Professionalize(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        return response;
    }
}