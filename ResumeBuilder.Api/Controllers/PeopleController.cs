#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumeBuilder.Api.Controllers;

public sealed class PersonController(IServiceProvider serviceProvider, IPeopleService peopleService)
    : BaseController(serviceProvider), IPersonController
{
    [HttpGet("{personId}")]
    public async Task<PersonaDetails> GetPerson()
    {
        return await peopleService.GetPerson<PersonaDetails>(OrganizationId, UserId)
            .ConfigureAwait(false);
    }

}