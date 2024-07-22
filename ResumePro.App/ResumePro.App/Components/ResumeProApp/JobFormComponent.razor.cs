using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class JobFormComponent: FormComponent<JobOptions>
    {
        void AddHighlight()
        {
            Options.HighlightOptions.Add(new HighlightOptions());
        }

        void RemoveHighlight(HighlightOptions highlight)
        {
            Options.HighlightOptions.Remove(highlight);
        }

        void AddProjectHighlight(ProjectOptions options)
        {
            options.HighlightOptions.Add(new HighlightOptions());
        }

        void RemoveProjectHighlight(ProjectOptions project, HighlightOptions options)
        {
            project.HighlightOptions.Remove(options);
        }

        void AddProject()
        {
            Options.ProjectOptions.Add(new ProjectOptions());
        }

        void RemoveProject(ProjectOptions project)
        {
            Options.ProjectOptions.Remove(project);
        }
    }
}
