namespace ResumePro.Api.Interfaces;

public interface IProjectsController
{
    Task<ProjectDetails> GetProject(int personId, int companyId, int positionId,
        int projectId);

    Task<List<ProjectDetails>> GetList(int personId, int companyId, int positionId);

    Task<ActionResult<ProjectDetails>> CreateProject(int personId, int companyId, int positionId,
        ProjectOptions options);

    Task<ActionResult<ProjectDetails>> Update(int personId, int companyId, int positionId,
        int projectId, ProjectOptions options);

    Task<Result> Delete(int personId, int companyId, int positionId,
        int projectId);
}