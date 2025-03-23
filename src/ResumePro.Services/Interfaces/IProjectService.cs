using Bespoke.Services.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.Services.Interfaces;

public interface IProjectService : IService<Project>
{
    Task<List<T>> GetProjects<T>(int organizationId, int personId, int companyId, int positionId) where T : ProjectDto;

    Task<T> GetProject<T>(int organizationId, int personId, int companyId, int positionId, int projectId)
        where T : ProjectDto;

    Task<OneOf<ProjectDetails, Result>> CreateProject(int organizationId, int personId, int companyId, int positionId,
        ProjectOptions options);

    Task<OneOf<ProjectDetails, Result>> UpdateProject(int organizationId, int personId, int companyId,
        int positionId1, int projectId,
        ProjectOptions options);

    Task<Result> DeleteProject(int organizationId, int personId, int companyId, int positionId, int projectId);
}