using ResumePro.Api.Interfaces;
using ResumePro.Services.Interfaces;

namespace ResumePro.Api.Controllers;

[Route("v1.0/people/{personId}/companies/{companyId}/positions/{positionId}/projects")]
public sealed class ProjectsController : BaseController, IProjectsController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IServiceProvider serviceProvider, IProjectService projectService) : base(serviceProvider)
    {
        _projectService = projectService;
    }

    [HttpGet("{projectId}")]
    public async Task<ProjectDetails> GetProject([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId, [FromRoute] int projectId)
    {
        return await _projectService
            .GetProject<ProjectDetails>(OrganizationId, personId, companyId, positionId, projectId)
            .ConfigureAwait(false);
    }

    [HttpGet]
    public async Task<List<ProjectDetails>> GetList([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId)
    {
        return await _projectService.GetProjects<ProjectDetails>(OrganizationId, positionId, companyId, positionId)
            .ConfigureAwait(false);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDetails>> CreateProject([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromBody] ProjectOptions options)
    {
        var result = await _projectService.CreateProject(OrganizationId, personId, companyId, positionId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpPut("{projectId}")]
    public async Task<ActionResult<ProjectDetails>> Update([FromRoute] int personId, [FromRoute] int companyId,
        [FromRoute] int positionId,
        [FromRoute] int projectId, [FromBody] ProjectOptions options)
    {
        var result = await _projectService
            .UpdateProject(OrganizationId, personId, companyId, positionId, projectId, options)
            .ConfigureAwait(false);
        if (result.IsT0) return Ok(result.AsT0);

        return BadRequest(result.AsT1);
    }

    [HttpDelete("{projectId}")]
    public async Task<Result> Delete([FromRoute] int personId, [FromRoute] int companyId, [FromRoute] int positionId,
        [FromRoute] int projectId)
    {
        return await _projectService.DeleteProject(OrganizationId, personId, companyId, positionId, projectId)
            .ConfigureAwait(false);
    }
}