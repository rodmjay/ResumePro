using Bespoke.IntegrationTesting.Bases;
using Bespoke.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using ResumePro.Api.Interfaces;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;

namespace ResumePro.IntegrationTests.Proxies;

public sealed class ProjectsProxy : BaseProxy, IProjectsController
{
    private const string RootUrl = "v1.0/people/{0}/companies/{1}/positions/{2}/projects";

    public ProjectsProxy(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ProjectDetails> GetProject(int personId, int companyId, int positionId, int projectId)
    {
        return await DoGetAsync<ProjectDetails>($"{string.Format(RootUrl, personId, companyId, positionId)}/{projectId}");
    }

    public async Task<List<ProjectDetails>> GetList(int personId, int companyId, int positionId)
    {
        return await DoGetAsync<List<ProjectDetails>>(string.Format(RootUrl, personId, companyId, positionId));
    }

    public async Task<ActionResult<ProjectDetails>> CreateProject(int personId, int companyId, int positionId,
        ProjectOptions options)
    {
        return await DoPostActionResultAsync<ProjectOptions, ProjectDetails>(string.Format(RootUrl, personId, companyId, positionId), options);
    }

    public async Task<ActionResult<ProjectDetails>> Update(int personId, int companyId, int positionId, int projectId,
        ProjectOptions options)
    {
        return await DoPutActionResultAsync<ProjectOptions, ProjectDetails>($"{string.Format(RootUrl, personId, companyId, positionId)}/{projectId}", options);
    }

    public async Task<Result> Delete(int personId, int companyId, int positionId, int projectId)
    {
        return await DoDeleteAsync<Result>($"{string.Format(RootUrl, personId, companyId, positionId)}/{projectId}");
    }
}
