#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

public class PeopleController : BaseController
{
    private readonly IPeopleService _peopleService;

    public PeopleController(IServiceProvider serviceProvider, IPeopleService peopleService) : base(serviceProvider)
    {
        _peopleService = peopleService;
    }

    [HttpPost("Search")]
    public Task<PagedList<PersonaDto>> GetPeople(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] PersonaFilters? filters, [FromQuery] PagingQuery paging)
    {
        return _peopleService.GetPeople<PersonaDto>(OrganizationId, filters, paging);
    }

    [HttpGet("{personId}")]
    public Task<PersonaDetails> GetPerson([FromRoute] int personId)
    {
        return _peopleService.GetPerson<PersonaDetails>(OrganizationId, personId);
    }

    [HttpPost]
    public async Task<ActionResult<PersonaDetails>> CreatePerson([FromBody] PersonaOptions options)
    {
        var result = await _peopleService.CreatePerson(OrganizationId, options);
        if (result.IsT0)
            return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }
}