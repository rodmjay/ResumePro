using System.Diagnostics;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumePro.Context;
using ResumePro.Core.Middleware.Extensions;
using ResumePro.Extensions;
using ResumePro.Services;
using ResumePro.Shared;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;
using System.Text;
using System.Drawing.Printing;

namespace ResumePro.Generator
{
    internal class Program
    {
        private static IServiceProvider serviceProvider;

        static Program()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            serviceProvider = services!.ConfigureApp(configuration)
                .AddDatabase<ApplicationContext>()
                .AddAutomapperProfilesFromAssemblies()
                .AddApplicationDependencies()
                .Build();
        }

        static void Main(string[] args)
        {
            IResumeService resumeService = serviceProvider.GetRequiredService<IResumeService>();

            ResumeDetails resume = resumeService.GetResume<ResumeDetails>(1).Result;

            var document = CreateResumePdf(resume);

            // Save the document...
            const string filename = "HelloWorld.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start("explorer", filename);


            Console.ReadLine();

        }


        public static PdfDocument CreateResumePdf(ResumeDetails resumeDetails)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = $"Resume - {resumeDetails.FirstName} {resumeDetails.LastName}";

            const double margin = 40;
            const double pageWidth = 595;
            const double pageHeight = 842; // A4 size height

            XFont titleFont = new XFont("Verdana", 20, XFontStyleEx.Bold);
            XFont headerFont = new XFont("Verdana", 14, XFontStyleEx.Bold);
            XFont regularFont = new XFont("Verdana", 12, XFontStyleEx.Regular);

            double currentY = margin;
            XGraphics gfx = null;

            PdfPage AddNewPage()
            {
                var page = document.AddPage();
                gfx = XGraphics.FromPdfPage(page);
                currentY = margin;
                return page;
            }

            void DrawTitle(string title)
            {
                if (gfx == null || currentY > pageHeight - margin)
                {
                    AddNewPage();
                }
                XRect rect = new XRect(0, currentY, pageWidth, 100);
                gfx.DrawString(title, titleFont, XBrushes.Black, rect, XStringFormats.TopCenter);
                currentY += titleFont.Height + 10; // Adjust spacing after title
            }

            void DrawHeader(string header)
            {
                if (gfx == null || currentY > pageHeight - margin)
                {
                    AddNewPage();
                }
                currentY += 10; // Add space before header
                XRect rect = new XRect(margin, currentY, pageWidth - margin * 2, 100);
                gfx.DrawString(header, headerFont, XBrushes.Black, rect, XStringFormats.TopLeft);
                currentY += headerFont.Height + 5; // Adjust spacing after header
            }

            void DrawText(string text, double indentation = 0)
            {
                if (string.IsNullOrWhiteSpace(text))
                    return;

                const double lineHeight = 12; // Adjust as needed for line height
                const double lineSpacing = 5; // Adjust as needed for line spacing

                XRect rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, pageHeight);

                // Split text into lines that fit within the available width
                List<string> lines = SplitTextToFit(gfx, text, regularFont, rect.Width);

                foreach (string line in lines)
                {
                    if (currentY > pageHeight - margin)
                    {
                        AddNewPage();
                        rect = new XRect(margin + indentation, currentY, pageWidth - margin * 2 - indentation, pageHeight);
                    }
                    gfx.DrawString(line, regularFont, XBrushes.Black, rect, XStringFormats.TopLeft);
                    rect = new XRect(rect.Left, rect.Top + lineHeight + lineSpacing, rect.Width, rect.Height - lineHeight - lineSpacing);
                    currentY += lineHeight + lineSpacing;
                }
            }

            List<string> SplitTextToFit(XGraphics gfx, string text, XFont font, double maxWidth)
            {
                List<string> lines = new List<string>();
                StringBuilder currentLine = new StringBuilder();
                string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (gfx.MeasureString(currentLine.ToString() + " " + word, font).Width <= maxWidth)
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
                }

                if (currentLine.Length > 0)
                    lines.Add(currentLine.ToString());

                return lines;
            }

            // Add sections
            foreach (var section in GetResumeSections(resumeDetails))
            {
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
                }
            }

            return document;
        }

        private static IEnumerable<ResumeSection> GetResumeSections(ResumeDetails resumeDetails)
        {
            yield return new ResumeSection { SectionType = ResumeSectionType.Title, Text = $"{resumeDetails.FirstName} {resumeDetails.LastName}" };

            yield return new ResumeSection { SectionType = ResumeSectionType.Header, Text = "Contact Information" };
            yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"Email: {resumeDetails.Email}" };
            yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"Phone: {resumeDetails.PhoneNumber}" };
            yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"LinkedIn: {resumeDetails.LinkedIn}" };
            yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"GitHub: {resumeDetails.GitHub}" };

            yield return new ResumeSection { SectionType = ResumeSectionType.Header, Text = "Description" };
            yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = resumeDetails.Description };

            yield return new ResumeSection { SectionType = ResumeSectionType.Header, Text = "Skills" };
            foreach (var skill in resumeDetails.Skills)
            {
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"- {skill.Title} (Rating: {skill.Rating})", Indentation = 10 };
            }

            yield return new ResumeSection { SectionType = ResumeSectionType.Header, Text = "Experience" };
            foreach (var job in resumeDetails.Jobs)
            {
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"{job.Company} - {job.Title}", Indentation = 10 };
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"{job.StartDate.ToShortDateString()} - {(job.EndDate.HasValue ? job.EndDate.Value.ToShortDateString() : "Present")}", Indentation = 20 };
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = job.Description, Indentation = 20 };

                foreach (var highlight in job.Highlights)
                {
                    yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"- {highlight.Text}", Indentation = 30 };
                }

                foreach (var project in job.Projects)
                {
                    yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"Project: {project.Name}", Indentation = 30 };
                    yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = project.Description, Indentation = 40 };

                    foreach (var projectHighlight in project.Highlights)
                    {
                        yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"- {projectHighlight.Text}", Indentation = 50 };
                    }
                }
            }

            yield return new ResumeSection { SectionType = ResumeSectionType.Header, Text = "Education" };
            foreach (var school in resumeDetails.Education)
            {
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"{school.Name}", Indentation = 10 };
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"{school.StartDate.ToShortDateString()} - {(school.EndDate.HasValue ? school.EndDate.Value.ToShortDateString() : "Present")}", Indentation = 20 };
                foreach (var degree in school.Degrees)
                {
                    yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"Degree: {degree.Name}", Indentation = 20 };
                }
            }

            yield return new ResumeSection { SectionType = ResumeSectionType.Header, Text = "References" };
            foreach (var reference in resumeDetails.References)
            {
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = $"{reference.Name} - {reference.PhoneNumber}", Indentation = 10 };
                yield return new ResumeSection { SectionType = ResumeSectionType.Text, Text = reference.Text, Indentation = 20 };
            }
        }

        public enum ResumeSectionType
        {
            Title,
            Header,
            Text
        }

        public class ResumeSection
        {
            public ResumeSectionType SectionType { get; set; }
            public string Text { get; set; }
            public double Indentation { get; set; }
        }



    }
}

