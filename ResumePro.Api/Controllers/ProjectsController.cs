#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared.Interfaces;
using ResumePro.Shared.Models;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/projects")]
public sealed class ProjectsController(IServiceProvider serviceProvider, IProjectService projectService)
    : BaseController(serviceProvider), IProjectsController
{
    [HttpGet("{projectId}")]
    public async Task<ProjectDetails> GetProject([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId)
    {
        return await projectService.GetProject<ProjectDetails>(OrganizationId, projectId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ProjectDetails>> GetList([FromRoute] int personId, [FromRoute] int jobId)
    {
        return await projectService.GetProjects<ProjectDetails>(OrganizationId, jobId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDetails>> CreateProject([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] ProjectOptions options)
    {
        var result = await projectService.CreateProject(OrganizationId, jobId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{projectId}")]
    public async Task<ActionResult<ProjectDetails>> Update([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId, [FromBody] ProjectOptions options)
    {
        var result = await projectService.UpdateProject(OrganizationId, jobId, projectId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{projectId}")]
    public async Task<Result> Delete([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId)
    {
        return await projectService.DeleteProject(OrganizationId, jobId, projectId)
            .ConfigureAwait(false);
    }
}