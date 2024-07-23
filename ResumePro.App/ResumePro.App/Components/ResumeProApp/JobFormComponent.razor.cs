#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using ResumePro.App.Components.ResumeProApp.Bases;
using ResumePro.Shared.Options;

namespace ResumePro.App.Components.ResumeProApp;

public partial class JobFormComponent : FormComponent<JobOptions>
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

    private void MoveProjectUp(int index)
    {
        if (index >= 0 && index < Options.ProjectOptions.Count - 1)
        {
            var item = Options.ProjectOptions[index];
            Options.ProjectOptions.RemoveAt(index);
            Options.ProjectOptions.Insert(index + 1, item);
        }
    }

    private void MoveProjectDown(int index)
    {
        if (index >= 0 && index < Options.ProjectOptions.Count - 1)
        {
            var item = Options.ProjectOptions[index];
            Options.ProjectOptions.RemoveAt(index);
            Options.ProjectOptions.Insert(index + 1, item);
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

    private void AddHighlight()
    {
        Options.HighlightOptions.Add(new HighlightOptions());
    }

    private void RemoveHighlight(HighlightOptions highlight)
    {
        Options.HighlightOptions.Remove(highlight);
    }

    private void AddProjectHighlight(ProjectOptions options)
    {
        options.HighlightOptions.Add(new HighlightOptions());
    }

    private void RemoveProjectHighlight(ProjectOptions project, HighlightOptions options)
    {
        project.HighlightOptions.Remove(options);
    }

    private void AddProject()
    {
        Options.ProjectOptions.Add(new ProjectOptions());
    }

    private void RemoveProject(ProjectOptions project)
    {
        Options.ProjectOptions.Remove(project);
    }
}