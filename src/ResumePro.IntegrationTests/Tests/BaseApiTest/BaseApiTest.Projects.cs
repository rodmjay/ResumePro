using NUnit.Framework;
using ResumePro.Shared.Models;
using ResumePro.Shared.Options;
using Bespoke.IntegrationTesting.Extensions;
using Bespoke.Shared.Common;

namespace ResumePro.IntegrationTests.Tests
{
    public abstract partial class BaseApiTest
    {
        protected async Task<ProjectDetails> AssertGetProject(int personId, int companyId, int positionId, int projectId)
        {
            var project = await ProjectsController.GetProject(personId, companyId, positionId, projectId);
            Assert.That(project, Is.Not.Null, "Failed to retrieve project");
            return project;
        }

        protected async Task<List<ProjectDetails>> AssertGetProjects(int personId, int companyId, int positionId)
        {
            var projects = await ProjectsController.GetList(personId, companyId, positionId);
            Assert.That(projects, Is.Not.Null, "Failed to retrieve projects");
            return projects;
        }

        protected async Task<ProjectDetails> AssertCreateProject(int personId, int companyId, int positionId, ProjectOptions options)
        {
            var response = await ProjectsController.CreateProject(personId, companyId, positionId, options);
            Assert.That(response.IsSuccessStatusCode(), "Project creation failed");
            var project = response.GetObject<ProjectDetails>();
            return project;
        }

        protected async Task<ProjectDetails> AssertUpdateProject(int personId, int companyId, int positionId, int projectId, ProjectOptions options)
        {
            var response = await ProjectsController.Update(personId, companyId, positionId, projectId, options);
            Assert.That(response.IsSuccessStatusCode(), "Project update failed");
            var project = response.GetObject<ProjectDetails>();
            return project;
        }

        protected async Task<Result> AssertDeleteProject(int personId, int companyId, int positionId, int projectId)
        {
            var result = await ProjectsController.Delete(personId, companyId, positionId, projectId);
            Assert.That(result.Succeeded, "Project deletion failed");
            return result;
        }
    }
}
