#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Text;
using ResumePro.Shared;

namespace ResumePro.Generator.Strategies;

public class MarkupResumeStrategy : IResumeStrategy
{
    private readonly MarkupSettings _settings;
    private const string ReadMePath = @"..\..\..\..\readme.md";
    

    public MarkupResumeStrategy(MarkupSettings settings)
    {
        _settings = settings;
    }

    public void ExecuteOperation(ResumeDetails resumeDetails)
    {
        var resumeText = BuildResumeMarkdown(resumeDetails);

        if(_settings.UpdateReadme)
            UpdateReadMe(resumeText);

        if (_settings.OutputToConsole)
            Console.WriteLine(resumeText);
    }

    private string BuildResumeMarkdown(ResumeDetails resumeDetails)
    {
        var sb = new StringBuilder();

        // Title
        sb.AppendLine($"# {resumeDetails.FirstName} {resumeDetails.LastName}, {resumeDetails.JobTitle}");
        sb.AppendLine();

        // Contact Information
        sb.AppendLine("## Contact Information");
        sb.AppendLine($"- **Email:** {resumeDetails.Email}");
        sb.AppendLine($"- **Phone:** {resumeDetails.PhoneNumber}");
        sb.AppendLine($"- **LinkedIn:** {resumeDetails.LinkedIn}");
        sb.AppendLine($"- **GitHub:** {resumeDetails.GitHub}");
        sb.AppendLine();

        // Description
        sb.AppendLine("## Description");
        sb.AppendLine(resumeDetails.Description);
        sb.AppendLine();

        // Skills
        sb.AppendLine("## Skills");
        foreach (var skill in resumeDetails.Skills) sb.AppendLine($"- {skill.Title} (Rating: {skill.Rating})");
        sb.AppendLine();

        // Experience
        sb.AppendLine("## Experience");
        foreach (var job in resumeDetails.Jobs)
        {
            sb.AppendLine($"### {job.Company} - {job.Title}");
            sb.AppendLine(
                $"*{job.StartDate.ToShortDateString()} - {(job.EndDate.HasValue ? job.EndDate.Value.ToShortDateString() : "Present")}*");
            sb.AppendLine(job.Description);
            sb.AppendLine();

            foreach (var highlight in job.Highlights)
            {
                sb.AppendLine($"- {highlight.Text}");
                sb.AppendLine();
            }

            if (job.Projects.Any())
                foreach (var project in job.Projects)
                {
                    sb.AppendLine($"#### Project: {project.Name}");
                    sb.AppendLine(project.Description);

                    foreach (var projectHighlight in project.Highlights) sb.AppendLine($"- {projectHighlight.Text}");
                    sb.AppendLine();
                }

            if (job.Skills != null && job.Skills.Any())
                sb.AppendLine($"**Technology Used:** {string.Join(", ", job.Skills.Select(s => s.Name))}");

            sb.AppendLine();
        }

        // Education
        sb.AppendLine("## Education");
        foreach (var school in resumeDetails.Education)
        {
            sb.AppendLine($"### {school.Name}");
            sb.AppendLine(
                $"*{school.StartDate.ToShortDateString()} - {(school.EndDate.HasValue ? school.EndDate.Value.ToShortDateString() : "Present")}*");
            foreach (var degree in school.Degrees) sb.AppendLine($"- Degree: {degree.Name}");
            sb.AppendLine();
        }

        // References
        sb.AppendLine("## References");
        foreach (var reference in resumeDetails.References)
        {
            sb.AppendLine($"### {reference.Name}");
            sb.AppendLine(reference.Text);
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private void UpdateReadMe(string resumeText)
    {
        var fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ReadMePath));

        if (File.Exists(fullPath))
        {
            File.WriteAllText(fullPath, resumeText);
            Console.WriteLine("readme file updated successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}