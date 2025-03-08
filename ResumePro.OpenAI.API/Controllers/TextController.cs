#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.AI.Services;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.OpenAI.Api.Controllers;

public class TextController : BaseController, ITextController
{
    private IChatGptService _service;

    public TextController(IServiceProvider serviceProvider, IChatGptService service) : base(serviceProvider)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ChatResult> Professionalize([FromBody] ChatOptions options)
    {
        var response = await _service.Professionalize(OrganizationId, UserId, options)
            .ConfigureAwait(false);
        return response;
    }
}