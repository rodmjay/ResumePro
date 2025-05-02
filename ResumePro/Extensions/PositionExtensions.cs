#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

namespace ResumePro.Extensions;

internal static class PositionExtensions
{
    public static Position Create(this PositionOptions options, Func<Task<int>> newId)
    {
        var id = newId().Result;

        var position = new Position()
        {
            Id = id,
            StartDate = options.StartDate,
            EndDate = options.EndDate,
            ObjectState = ObjectState.Added,
            JobTitle = options.JobTitle
        };

        position.AddHighlights(options.Highlights);
        position.CreateProjects(options.Projects);

        return position;
    }


    public static Position Update(this Position position, PositionOptions options)
    {
        position.JobTitle = options.JobTitle;
        position.StartDate = options.StartDate;
        position.EndDate = options.EndDate;
        position.ObjectState = ObjectState.Modified;

        position.UpdateHighlights(options.Highlights);
        position.UpdateProjects(options.Projects);

        return position;
    }

    public static Position AddHighlights(this Position position, List<HighlightOptions> options)
    {
        for (var j = 0; j < options.Count; j++)
        {
            position.AddHighlight(options[j], j);
        }

        return position;
    }

    public static Position AddHighlight(this Position position, HighlightOptions highlightOptions, int index)
    {
        position.Highlights.Add(highlightOptions.Create(()=>Task.FromResult(index + 1)));

        return position;
    }

    public static Position UpdateHighlights(this Position position, List<HighlightOptions> highlightOptions)
    {
        if (highlightOptions.Count > 0)
        {
            foreach (var highlight in position.Highlights)
            {
                highlight.ObjectState = ObjectState.Deleted;
            }
        }

        for (var index = 0; index < highlightOptions.Count; index++)
        {
            var highlightOption = highlightOptions[index];
            var highlight = position.Highlights.FirstOrDefault(x => x.Id == highlightOption.Id);
            if (highlight == null)
            {
                position.AddHighlight(highlightOption, index);
            }
            else
            {
                highlight.Update(highlightOption, index);
            }

        }

        return position;
    }

    public static Position CreateProjects(this Position position, List<ProjectOptions> projectOptions)
    {
        if (projectOptions.Count > 0)
        {
            foreach (var project in position.Projects)
            {
                project.ObjectState = ObjectState.Deleted;
            }
        }

        for (int i = 0; i < projectOptions.Count; i++)
        {
            position.AddProject(projectOptions[i], i);
        }

        return position;
    }

    public static Position AddProject(this Position position, ProjectOptions options, int index)
    {
        Project project = options.Create(index);

        position.Projects.Add(project);

        return position;
    }


    public static Position UpdateProjects(this Position position, List<ProjectOptions> projectOptions)
    {
        if (projectOptions.Count > 0)
        {
            foreach (var project in position.Projects)
            {
                project.ObjectState = ObjectState.Deleted;
            }
        }

        for (var index = 0; index < projectOptions.Count; index++)
        {
            var projectOption = projectOptions[index];
            var project = position.Projects.FirstOrDefault(x => x.Id == projectOption.Id);
            if (project == null)
            {
                position.AddProject(projectOption, index);
            }
            else
            {
                project.Update(projectOption, index);
            }
        }

        return position;
    }
}