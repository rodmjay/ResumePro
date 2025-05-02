#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.AI.Services;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;
using TechValley.Rest.Bases;

namespace ResumePro.OpenAI.Api.Controllers;

public class TextController(IServiceProvider serviceProvider, IChatGptService service)
    : ExtendedBaseController(serviceProvider), ITextController
{
    [HttpPost]
    public async Task<ChatResult> Professionalize([FromBody] ChatOptions options)
    {
        var response = await service.Professionalize(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        return response;
    }
}