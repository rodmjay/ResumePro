#region Header Info

// Copyright 2024 Rod Johnson.  All rights reserved

#endregion

using System.Diagnostics;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using ResumePro.Shared;

namespace ResumePro.Generator.Strategies;

public class PdfResumeStrategy : IResumeStrategy
{
    private readonly PdfSettings _settings;

    public enum ResumeSectionType
    {
        Title,
        Header,
        Text,
        BoldText,
        ItalicText
    }
    

    public PdfResumeStrategy(PdfSettings settings)
    {
        _settings = settings;
    }


    public void ExecuteOperation(ResumeDetails resumeDetails)
    {
        PdfDocument document = BuildResumePdf(resumeDetails);

        if (_settings.CreateUpdatePdf)
        {
            string fileName = SaveResume(document, resumeDetails);
            if (_settings.DisplayInExplorer) Process.Start("explorer", fileName);
        }
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


        string languages = string.Join(", ", resumeDetails.Languages.OrderByDescending(a => a.Proficiency)
            .Select(language => $"{language.LanguageName}").ToList());

        yield return new ResumeSection {SectionType = ResumeSectionType.Text, Text = $"Languages: {languages}"};

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Description"};
        yield return new ResumeSection {SectionType = ResumeSectionType.Text, Text = resumeDetails.Description};

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Skills"};
        foreach (ResumeSkillDto? skill in resumeDetails.Skills)
            yield return new ResumeSection
            {
                SectionType = ResumeSectionType.Text,
                Text = $"- {skill.Title} (Rating: {skill.Rating})",
                Indentation = 10
            };

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Experience"};
        foreach (JobDetails? job in resumeDetails.Jobs)
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

            foreach (HighlightDto? highlight in job.Highlights)
                yield return new ResumeSection
                    {SectionType = ResumeSectionType.Text, Text = $"- {highlight.Text}", Indentation = 30};

            if (job.Projects.Any())
                foreach (ProjectDetails? project in job.Projects)
                {
                    yield return new ResumeSection
                        {SectionType = ResumeSectionType.BoldText, Text = $"Project: {project.Name}", Indentation = 30};
                    yield return new ResumeSection
                        {SectionType = ResumeSectionType.Text, Text = project.Description, Indentation = 40};

                    foreach (HighlightDto? projectHighlight in project.Highlights)
                        yield return new ResumeSection
                        {
                            SectionType = ResumeSectionType.Text,
                            Text = $"- {projectHighlight.Text}",
                            Indentation = 50
                        };
                }

            if (job.Skills != null && job.Skills.Any())
            {
                string skillsText = "Technology Used: " + string.Join(", ", job.Skills.Select(s => s.Name));
                yield return new ResumeSection
                    {SectionType = ResumeSectionType.ItalicText, Text = skillsText, Indentation = 10};
            }

            // Add space between jobs
            yield return new ResumeSection
                {SectionType = ResumeSectionType.Text, Text = string.Empty, Indentation = 10};
        }

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "Education"};
        foreach (SchoolDetails? school in resumeDetails.Education)
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
            foreach (DegreeDto? degree in school.Degrees)
                yield return new ResumeSection
                    {SectionType = ResumeSectionType.Text, Text = $"Degree: {degree.Name}", Indentation = 20};
        }

        yield return new ResumeSection {SectionType = ResumeSectionType.Header, Text = "References"};
        foreach (ReferenceDto? reference in resumeDetails.References)
        {
            yield return new ResumeSection
                {SectionType = ResumeSectionType.ItalicText, Text = $"{reference.Name}", Indentation = 10};
            yield return new ResumeSection
                {SectionType = ResumeSectionType.Text, Text = reference.Text, Indentation = 20};
        }

        yield return new ResumeSection
        {
            SectionType = ResumeSectionType.ItalicText,
            Text = "More Info: https://www.github.com/rodmjay/resumepro."
        };
    }


    private PdfDocument BuildResumePdf(ResumeDetails resumeDetails)
    {
        PdfDocument document = new PdfDocument();
        document.Info.Title = $"Resume - {resumeDetails.FirstName} {resumeDetails.LastName}";

        const double margin = 40;
        const double pageWidth = 595;
        const double pageHeight = 842; // A4 size height
        const double bottomMargin = 60;

        XFont titleFont = new XFont(_settings.FontFamily, 20, XFontStyleEx.Bold);
        XFont headerFont = new XFont(_settings.FontFamily, 14, XFontStyleEx.Bold);
        XFont regularFont = new XFont(_settings.FontFamily, 12, XFontStyleEx.Regular);
        XFont italicFont = new XFont(_settings.FontFamily, 12, XFontStyleEx.Italic);
        XFont boldFont = new XFont(_settings.FontFamily, 12, XFontStyleEx.Bold);

        double currentY = margin;
        XGraphics gfx = null;

        PdfPage AddNewPage()
        {
            PdfPage page = document.AddPage();
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
            XRect rect = new XRect(0, currentY, pageWidth, 100);
            gfx.DrawString(title, titleFont, XBrushes.Black, rect, XStringFormats.TopCenter);
            currentY += titleFont.Height + 10; // Adjust spacing after title
        }

        void DrawHeader(string header)
        {
            EnsureSpaceForText(headerFont.Height + 15);
            currentY += 20; // Add space before header
            XRect rect = new XRect(margin, currentY, pageWidth - margin * 2, 100);
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

            List<string> lines = SplitTextToFit(gfx, text, boldFont, pageWidth - margin * 2 - indentation);

            foreach (string line in lines)
            {
                EnsureSpaceForText(lineHeight + lineSpacing);
                XRect rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, lineHeight);
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

            List<string> lines = SplitTextToFit(gfx, text, regularFont, pageWidth - margin * 2 - indentation);

            foreach (string line in lines)
            {
                EnsureSpaceForText(lineHeight + lineSpacing);
                XRect rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, lineHeight);
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

            List<string> lines = SplitTextToFit(gfx, text, italicFont, pageWidth - margin * 2 - indentation);

            foreach (string line in lines)
            {
                EnsureSpaceForText(lineHeight + lineSpacing);
                XRect rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, lineHeight);
                gfx.DrawString(line, italicFont, XBrushes.Black, rect, XStringFormats.TopLeft);
                currentY += lineHeight + lineSpacing;
            }
        }

        List<string> SplitTextToFit(XGraphics gfx, string text, XFont font, double maxWidth)
        {
            List<string> lines = new List<string>();
            StringBuilder currentLine = new StringBuilder();
            string[] words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
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
        foreach (ResumeSection section in GetResumeSections(resumeDetails))
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

    private string SaveResume(PdfDocument document, ResumeDetails resumeDetails)
    {
        string fileName = $"{resumeDetails.FirstName} {resumeDetails.LastName}-{resumeDetails.JobTitle}.pdf";
        string relativePath = $@"..\..\..\..\{fileName}";
        string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));

        document.Save(absolutePath);

        Console.WriteLine("File saved successfully.");

        return absolutePath;
    }

    public class ResumeSection
    {
        public ResumeSectionType SectionType { get; set; }
        public string Text { get; set; }
        public double Indentation { get; set; }
    }
}