namespace ResumePro.Extensions
{
    public static class ProjectExtensions
    {
        public static Project Update(this Project project, ProjectOptions options, int index)
        {

            project.Name = options.Name;
            project.Description = options.Description;
            project.Budget = options.Budget;
            project.Order = index + 1;
            project.ObjectState = ObjectState.Modified;

            project.UpdateHighlights(options.Highlights);

            return project;
        }

        public static Project Create(this ProjectOptions options, int index)
        {
            Project project = new Project
            {
                ObjectState = ObjectState.Added,
                Order = index + 1,
                Name = options.Name,
                Description = options.Description,
                Budget = options.Budget,
                Id = index + 1
            };

            project.AddHighlights(options.Highlights);

            return project;
        }

        public static Project AddHighlights(this Project project, List<HighlightOptions> highlights)
        {
            for (int index = 0; index < highlights.Count; index++)
            {
                HighlightOptions highlight = highlights[index];
                project.AddHighlight(highlight, index);
            }

            return project;
        }

        public static Project AddHighlight(this Project project, HighlightOptions options, int index)
        {
            ProjectHighlight highlight = new ProjectHighlight
            {
                ObjectState = ObjectState.Added,
                Id = index + 1,
                Text = options.Text,
                Order = index + 1
            };

            project.Highlights.Add(highlight);

            return project;
        }

        public static ProjectHighlight Update(this ProjectHighlight highlight, HighlightOptions options, int index)
        {
            highlight.ObjectState = ObjectState.Modified;
            highlight.Text = options.Text;
            highlight.Order = index + 1;
            return highlight;
        }

        public static Project UpdateHighlights(this Project project, List<HighlightOptions> highlightOptions)
        {

            for (int index = 0; index < highlightOptions.Count; index++)
            {
                HighlightOptions highlightOption = highlightOptions[index];
                ProjectHighlight highlight = project.Highlights.FirstOrDefault(x => x.Id == highlightOption.Id);
                if (highlight == null)
                {
                    project.AddHighlight(highlightOption, index);
                }
                else
                {
                    highlight.Update(highlightOption, index);
                }
            }

            return project;
        }
    }
}
