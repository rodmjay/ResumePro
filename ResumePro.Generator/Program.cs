#region Header Info

// Copyright 2023 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Extensions;
using ResumePro.Services;
using ResumePro.Shared;

namespace ResumePro.Generator;

internal class Program
{
    public enum ResumeSectionType
    {
        Title,
        Header,
        Text,
        BoldText,
        ItalicText
    }

    private static readonly IServiceProvider serviceProvider;

    static Program()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", false, true)
            .Build();

        var services = new ServiceCollection();

        serviceProvider = services!.ConfigureApp(configuration)
            .AddDatabase<ApplicationContext>()
            .AddAutomapperProfilesFromAssemblies()
            .AddApplicationDependencies()
            .Build();
    }

    private static void Main(string[] args)
    {
        var resumeService = serviceProvider.GetRequiredService<IResumeService>();

        var resume = resumeService.GetResume<ResumeDetails>(1).Result;

        var document = CreateResumePdf(resume);

        //var fn = resume.FirstName + " " + resume.LastName + "-" + resume.JobTitle + ".pdf";

        //document.Save(fn);
        //// ...and start a viewer.
        //Process.Start("explorer", fn);


        // Example usage
        string markdownResume = GenerateMarkdownResume(resume);
        Console.WriteLine(markdownResume);



        Console.ReadLine();
    }
    

    public static string GenerateMarkdownResume(ResumeDetails resumeDetails)
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
        foreach (var skill in resumeDetails.Skills)
        {
            sb.AppendLine($"- {skill.Title} (Rating: {skill.Rating})");
        }
        sb.AppendLine();

        // Experience
        sb.AppendLine("## Experience");
        foreach (var job in resumeDetails.Jobs)
        {
            sb.AppendLine($"### {job.Company} - {job.Title}");
            sb.AppendLine($"*{job.StartDate.ToShortDateString()} - {(job.EndDate.HasValue ? job.EndDate.Value.ToShortDateString() : "Present")}*");
            sb.AppendLine(job.Description);
            sb.AppendLine();

            foreach (var highlight in job.Highlights)
            {
                sb.AppendLine($"- {highlight.Text}");
            }

            if (job.Projects.Any())
            {
                foreach (var project in job.Projects)
                {
                    sb.AppendLine($"#### Project: {project.Name}");
                    sb.AppendLine(project.Description);

                    foreach (var projectHighlight in project.Highlights)
                    {
                        sb.AppendLine($"- {projectHighlight.Text}");
                    }
                    sb.AppendLine();
                }
            }

            if (job.Skills != null && job.Skills.Any())
            {
                sb.AppendLine($"**Technology Used:** {string.Join(", ", job.Skills.Select(s => s.Title))}");
            }

            sb.AppendLine();
        }

        // Education
        sb.AppendLine("## Education");
        foreach (var school in resumeDetails.Education)
        {
            sb.AppendLine($"### {school.Name}");
            sb.AppendLine($"*{school.StartDate.ToShortDateString()} - {(school.EndDate.HasValue ? school.EndDate.Value.ToShortDateString() : "Present")}*");
            foreach (var degree in school.Degrees)
            {
                sb.AppendLine($"- Degree: {degree.Name}");
            }
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


    public static PdfDocument CreateResumePdf(ResumeDetails resumeDetails)
    {
        var document = new PdfDocument();
        document.Info.Title = $"Resume - {resumeDetails.FirstName} {resumeDetails.LastName}";

        const double margin = 40;
        const double pageWidth = 595;
        const double pageHeight = 842; // A4 size height
        const double bottomMargin = 60;

        var titleFont = new XFont("Verdana", 20, XFontStyleEx.Bold);
        var headerFont = new XFont("Verdana", 14, XFontStyleEx.Bold);
        var regularFont = new XFont("Verdana", 12, XFontStyleEx.Regular);
        var italicFont = new XFont("Verdana", 12, XFontStyleEx.Italic);
        var boldFont = new XFont("Verdana", 12, XFontStyleEx.Bold);

        var currentY = margin;
        XGraphics gfx = null;

        PdfPage AddNewPage()
        {
            var page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            currentY = margin;
            return page;
        }

        // Ensure gfx is initialized at the start
        AddNewPage();

        void EnsureSpaceForText(double lineHeight)
        {
            if (currentY + lineHeight > pageHeight - bottomMargin) AddNewPage();
        }

        void DrawTitle(string title)
        {
            EnsureSpaceForText(titleFont.Height + 10);
            var rect = new XRect(0, currentY, pageWidth, 100);
            gfx.DrawString(title, titleFont, XBrushes.Black, rect, XStringFormats.TopCenter);
            currentY += titleFont.Height + 10; // Adjust spacing after title
        }

        void DrawHeader(string header)
        {
            EnsureSpaceForText(headerFont.Height + 15);
            currentY += 20; // Add space before header
            var rect = new XRect(margin, currentY, pageWidth - margin * 2, 100);
            gfx.DrawString(header, headerFont, XBrushes.Black, rect, XStringFormats.TopLeft);
            currentY += headerFont.Height + 5; // Adjust spacing after header
        }

        void DrawBoldText(string text, double indentation = 0)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            const double lineHeight = 12; // Adjust as needed for line height
            const double lineSpacing = 5; // Adjust as needed for line spacing
            currentY += 10; // Add space before header

            var lines = SplitTextToFit(gfx, text, boldFont, pageWidth - margin * 2 - indentation);

            foreach (var line in lines)
            {
                EnsureSpaceForText(lineHeight + lineSpacing);
                var rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, lineHeight);
                gfx.DrawString(line, boldFont, XBrushes.Black, rect, XStringFormats.TopLeft);
                currentY += lineHeight + lineSpacing;
            }
        }

        void DrawText(string text, double indentation = 0)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            const double lineHeight = 12; // Adjust as needed for line height
            const double lineSpacing = 5; // Adjust as needed for line spacing

            var lines = SplitTextToFit(gfx, text, regularFont, pageWidth - margin * 2 - indentation);

            foreach (var line in lines)
            {
                EnsureSpaceForText(lineHeight + lineSpacing);
                var rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, lineHeight);
                gfx.DrawString(line, regularFont, XBrushes.Black, rect, XStringFormats.TopLeft);
                currentY += lineHeight + lineSpacing;
            }
        }

        void DrawItalicText(string text, double indentation = 0)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            const double lineHeight = 12; // Adjust as needed for line height
            const double lineSpacing = 5; // Adjust as needed for line spacing
            currentY += 10;

            var lines = SplitTextToFit(gfx, text, italicFont, pageWidth - margin * 2 - indentation);

            foreach (var line in lines)
            {
                EnsureSpaceForText(lineHeight + lineSpacing);
                var rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, lineHeight);
                gfx.DrawString(line, italicFont, XBrushes.Black, rect, XStringFormats.TopLeft);
                currentY += lineHeight + lineSpacing;
            }
        }

        List<string> SplitTextToFit(XGraphics gfx, string text, XFont font, double maxWidth)
        {
            var lines = new List<string>();
            var currentLine = new StringBuilder();
            var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
                if (gfx.MeasureString(currentLine + " " + word, font).Width <= maxWidth)
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(" ");
                    currentLine.Append(word);
                }
                else
                {
                    lines.Add(currentLine.ToString());
                    currentLine = new StringBuilder(word);
                }

            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString());

            return lines;
        }

        // Add sections
        foreach (var section in GetResumeSections(resumeDetails))
            switch (section.SectionType)
            {
                case ResumeSectionType.Title:
                    DrawTitle(section.Text);
                    break;
                case ResumeSectionType.Header:
                    DrawHeader(section.Text);
                    break;
                case ResumeSectionType.Text:
                    DrawText(section.Text, section.Indentation);
                    break;
                case ResumeSectionType.BoldText:
                    DrawBoldText(section.Text, section.Indentation);
                    break;
                case ResumeSectionType.ItalicText:
                    DrawItalicText(section.Text, section.Indentation);
                    break;
            }

        return document;
    }

    private static IEnumerable<ResumeSection> GetResumeSections(ResumeDetails resumeDetails)
    {
        yield return new ResumeSection
        {
            SectionType = ResumeSectionType.Title,
            Text = $"{resumeDetails.FirstName} {resumeDetails.LastName}, {resumeDetails.JobTitle}"
        };

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Contact Information"};
        yield return new ResumeSection {SectionType = ResumeSectionType.Text, Text = $"Email: {resumeDetails.Email}"};
        yield return new ResumeSection
            {SectionType = ResumeSectionType.Text, Text = $"Phone: {resumeDetails.PhoneNumber}"};
        yield return new ResumeSection
            {SectionType = ResumeSectionType.Text, Text = $"LinkedIn: {resumeDetails.LinkedIn}"};
        yield return new ResumeSection {SectionType = ResumeSectionType.Text, Text = $"GitHub: {resumeDetails.GitHub}"};

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Description"};
        yield return new ResumeSection {SectionType = ResumeSectionType.Text, Text = resumeDetails.Description};

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Skills"};
        foreach (var skill in resumeDetails.Skills)
            yield return new ResumeSection
            {
                SectionType = ResumeSectionType.Text, Text = $"- {skill.Title} (Rating: {skill.Rating})",
                Indentation = 10
            };

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Experience"};
        foreach (var job in resumeDetails.Jobs)
        {
            yield return new ResumeSection
                {SectionType = ResumeSectionType.BoldText, Text = $"{job.Company} - {job.Title}", Indentation = 10};
            yield return new ResumeSection
            {
                SectionType = ResumeSectionType.Text,
                Text =
                    $"{job.StartDate.ToShortDateString()} - {(job.EndDate.HasValue ? job.EndDate.Value.ToShortDateString() : "Present")}",
                Indentation = 20
            };
            yield return new ResumeSection
                {SectionType = ResumeSectionType.Text, Text = job.Description, Indentation = 20};

            foreach (var highlight in job.Highlights)
                yield return new ResumeSection
                    {SectionType = ResumeSectionType.Text, Text = $"- {highlight.Text}", Indentation = 30};

            if (job.Projects.Any())
                foreach (var project in job.Projects)
                {
                    yield return new ResumeSection
                        {SectionType = ResumeSectionType.BoldText, Text = $"Project: {project.Name}", Indentation = 30};
                    yield return new ResumeSection
                        {SectionType = ResumeSectionType.Text, Text = project.Description, Indentation = 40};

                    foreach (var projectHighlight in project.Highlights)
                        yield return new ResumeSection
                        {
                            SectionType = ResumeSectionType.Text, Text = $"- {projectHighlight.Text}", Indentation = 50
                        };
                }

            if (job.Skills != null && job.Skills.Any())
            {
                var skillsText = "Technology Used: " + string.Join(", ", job.Skills.Select(s => s.Title));
                yield return new ResumeSection
                    {SectionType = ResumeSectionType.ItalicText, Text = skillsText, Indentation = 10};
            }

            // Add space between jobs
            yield return new ResumeSection
                {SectionType = ResumeSectionType.Text, Text = string.Empty, Indentation = 10};
        }

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Education"};
        foreach (var school in resumeDetails.Education)
        {
            yield return new ResumeSection
                {SectionType = ResumeSectionType.Text, Text = $"{school.Name}", Indentation = 10};
            yield return new ResumeSection
            {
                SectionType = ResumeSectionType.Text,
                Text =
                    $"{school.StartDate.ToShortDateString()} - {(school.EndDate.HasValue ? school.EndDate.Value.ToShortDateString() : "Present")}",
                Indentation = 20
            };
            foreach (var degree in school.Degrees)
                yield return new ResumeSection
                    {SectionType = ResumeSectionType.Text, Text = $"Degree: {degree.Name}", Indentation = 20};
        }

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "References"};
        foreach (var reference in resumeDetails.References)
        {
            yield return new ResumeSection
                {SectionType = ResumeSectionType.ItalicText, Text = $"{reference.Name}", Indentation = 10};
            yield return new ResumeSection
                {SectionType = ResumeSectionType.Text, Text = reference.Text, Indentation = 20};
        }
    }

    public class ResumeSection
    {
        public ResumeSectionType SectionType { get; set; }
        public string Text { get; set; }
        public double Indentation { get; set; }
    }
}