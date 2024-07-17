#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Filters;
using ResumePro.Shared.Interfaces;

namespace ResumePro.Api.Controllers;

public sealed class PeopleController(IServiceProvider serviceProvider, IPeopleService peopleService)
    : BaseController(serviceProvider), IPeopleController
{
    [HttpPost("Search")]
    public async Task<PagedList<PersonaDto>> GetPeople(
        [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]
        PersonaFilters? filters, [FromQuery] PagingQuery paging)
    {
        return await peopleService.GetPeople<PersonaDto>(OrganizationId, filters, paging)
            .ConfigureAwait(false);
    }

    [HttpGet("{personId}")]
    public async Task<PersonaDetails> GetPerson([FromRoute] int personId)
    {
        return await peopleService.GetPerson<PersonaDetails>(OrganizationId, personId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<PersonaDetails>> CreatePerson([FromBody] PersonaOptions options)
    {
        var result = await peopleService.CreatePerson(OrganizationId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
            return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{personId}")]
    public async Task<ActionResult<PersonaDetails>> UpdatePerson([FromRoute]int personId, [FromBody] PersonaOptions options)
    {
        var result = await peopleService.UpdatePerson(OrganizationId, personId, options)
            .ConfigureAwait(false);
        if (result.IsT0)
            return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{personId}")]
    public async Task<Result> DeletePerson([FromRoute] int personId)
    {
        return await peopleService.DeletePerson(OrganizationId, personId)
            .ConfigureAwait(false);
    }
}