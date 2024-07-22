using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp
{
    public partial class JobFormComponent: FormComponent<JobOptions>
    {
        private void MoveHighlightUp(int index)
        {
            if (index > 0 && index < Options.HighlightOptions.Count)
            {
                var item = Options.HighlightOptions[index];
                Options.HighlightOptions.RemoveAt(index);
                Options.HighlightOptions.Insert(index - 1, item);
            }
        }

        private void MoveProjectHighlightUp(ProjectOptions projectOptions, int index)
        {
            if (index > 0 && index < projectOptions.HighlightOptions.Count)
            {
                var item = projectOptions.HighlightOptions[index];
                projectOptions.HighlightOptions.RemoveAt(index);
                projectOptions.HighlightOptions.Insert(index - 1, item);
            }
        }
        private void MoveProjectHighlightDown(ProjectOptions projectOptions, int index)
        {
            if (index >= 0 && index < projectOptions.HighlightOptions.Count - 1)
            {
                var item = projectOptions.HighlightOptions[index];
                projectOptions.HighlightOptions.RemoveAt(index);
                projectOptions.HighlightOptions.Insert(index + 1, item);
            }
        }
        private void MoveHighlightDown(int index)
        {
            if (index >= 0 && index < Options.HighlightOptions.Count - 1)
            {
                var item = Options.HighlightOptions[index];
                Options.HighlightOptions.RemoveAt(index);
                Options.HighlightOptions.Insert(index + 1, item);
            }
        }
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
