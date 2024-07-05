#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using Microsoft.AspNetCore.Mvc;
using ResumePro.Core.Middleware.Bases;
using ResumePro.Interfaces;
using ResumePro.Shared;
using ResumePro.Shared.Common;
using ResumePro.Shared.Options;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/jobs/{jobId}/projects")]
public class ProjectsController : BaseController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IServiceProvider serviceProvider, IProjectService projectService) : base(serviceProvider)
    {
        _projectService = projectService;
    }

    [HttpGet("{projectId}")]
    public Task<ProjectDetails> GetProject([FromRoute] int personId, [FromRoute] int jobId, [FromRoute]int projectId)
    {
        return _projectService.GetProject<ProjectDetails>(OrganizationId, projectId);
    }

    [HttpGet]
    public Task<List<ProjectDetails>> GetList([FromRoute] int personId, [FromRoute] int jobId)
    {
        return _projectService.GetProjects<ProjectDetails>(OrganizationId, jobId);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDetails>> Create([FromRoute] int personId, [FromRoute] int jobId,
        [FromBody] ProjectOptions options)
    {
        var result = await _projectService.CreateProject(OrganizationId, jobId, options);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpPut("{projectId}")]
    public async Task<ActionResult<ProjectDetails>> Update([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute]int projectId, [FromBody] ProjectOptions options)
    {
        var result = await _projectService.UpdateProject(OrganizationId, jobId, projectId, options);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{projectId}")]
    public Task<Result> Delete([FromRoute] int personId, [FromRoute] int jobId,
        [FromRoute] int projectId)
    {
        return _projectService.DeleteProject(OrganizationId, jobId, projectId);
    }
}